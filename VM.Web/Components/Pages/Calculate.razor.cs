using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.Rendering;
using VM.Service.Models;
using VM.Service.Services;
using static VM.Service.Constants.Enums;

namespace VM.Web.Components.Pages
{
    public partial class Calculate
    {
        [Inject]
        private IVehicleTypeService? vehicleTypeService { get; set; }
        [Inject]
        private IFeesHeadService? feesHeadService { get; set; }
        [Inject]
        private IFeesStructureService? feesStructureService { get; set; }

        public List<SelectListItem> DropdownVehicleTypeList { get; set; } = new List<SelectListItem>();
        public List<FeesHeads> FeesHeadList { get; set; } = new List<FeesHeads>();
        public List<FeesStructures> FeesStructureList { get; set; } = new List<FeesStructures>();
        public CalculatePrice CalculatePrice { get; set; } = new CalculatePrice();
        public bool IsCalculated { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            await InitializeValues();
            StateHasChanged();
            await base.OnInitializedAsync();
        }

        private async Task InitializeValues()
        {
            if (vehicleTypeService is not null)
            {
                var response = await vehicleTypeService.GetAllAsync();
                if (response is not null && response.Success)
                {
                    DropdownVehicleTypeList.AddRange(response.Data.Select(x => new SelectListItem(x.Name, x.Id.ToString())));
                }
            }
            if (feesHeadService is not null)
            {
                var response = await feesHeadService.GetAllAsync();
                if (response is not null && response.Success)
                {
                    FeesHeadList = response.Data;
                }
            }
            
        }
        async Task TypeDropdownChange(ChangeEventArgs e)
        {
            string typeId = Convert.ToString(e.Value) ?? "";
            CalculatePrice.TypeId = !string.IsNullOrEmpty(typeId) ? Convert.ToInt32(typeId) : 0;
            await CalculateTotalPriceHandler(true);
        }
        async Task CalculateTotalPriceHandler(bool isTypeChanged = false)
        {
            if (CalculatePrice.VehiclePrice > 0 && CalculatePrice.TypeId > 0)
            {
                if ((isTypeChanged || FeesStructureList is null || !FeesStructureList.Any()) && feesStructureService is not null)
                {
                    var response = await feesStructureService.GetFeesStructureByTypeIdAsync(CalculatePrice.TypeId);
                    if (response is not null && response.Success)
                    {
                        FeesStructureList = response.Data;
                    }
                }

                if (FeesStructureList is not null && FeesStructureList.Any())
                {
                    foreach (var feeStructure in FeesStructureList)
                    {
                        if (feeStructure.IsFixOrPercentage == FeesStructureTypeEnum.Percentage)
                        {
                            double? fees = CalculatePrice.VehiclePrice * (feeStructure.Value / 100);
                            if (feeStructure.FeesHead.Name.ToLower().Contains(FeesStructureEnum.Basic.ToString().ToLower()))
                            {
                                fees = (fees < feeStructure.MinValue) ? feeStructure.MinValue : (fees > feeStructure.MaxValue ? feeStructure.MaxValue : fees);
                                CalculatePrice.BasicFee = Math.Round(fees ?? 0, 2);
                            }
                            else
                                CalculatePrice.SpecialFee = Math.Round(fees ?? 0, 2);
                        }
                        else
                        {
                            if (feeStructure.FeesHead.Name.ToLower().Contains(FeesStructureEnum.Association.ToString().ToLower()) && CalculatePrice.VehiclePrice >= feeStructure.MinValue && (CalculatePrice.VehiclePrice <= feeStructure.MaxValue || feeStructure.MaxValue is null))
                            {
                                CalculatePrice.AssociationFee = Math.Round(feeStructure.Value ?? 0, 2);
                            }
                            else if (feeStructure.FeesHead.Name.ToLower().Contains(FeesStructureEnum.Storage.ToString().ToLower()))
                            {
                                CalculatePrice.StorageFee = Math.Round(feeStructure.Value ?? 0, 2);
                            }
                        }
                    }
                    CalculatePrice.TotalPrice = CalculatePrice.VehiclePrice + CalculatePrice.BasicFee + CalculatePrice.SpecialFee + CalculatePrice.AssociationFee + CalculatePrice.StorageFee;
                    IsCalculated = true;
                }
                else
                    IsCalculated = false;
            }
            else
                IsCalculated = false;

            StateHasChanged();
        }
    }
}
