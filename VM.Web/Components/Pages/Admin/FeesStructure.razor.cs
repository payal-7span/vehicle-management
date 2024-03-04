using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using VM.Service.Models;
using VM.Service.Services;
using static VM.Service.Constants.Enums;

namespace VM.Web.Components.Pages.Admin
{
    public partial class FeesStructure
    {
        [Inject]
        private IFeesStructureService? feesStructureService { get; set; }
        [Inject]
        private IVehicleTypeService? vehicleTypeService { get; set; }
        [Inject]
        private IFeesHeadService? feesHeadService { get; set; }

        public List<FeesStructures> FeesStructureList { get; set; } = new List<FeesStructures>();
        public List<SelectListItem> DropdownVehicleTypeList { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> DropdownFeesHeadList { get; set; } = new List<SelectListItem>();

        public FeesStructures SelectedData { get; set; } = new FeesStructures();
        public bool ShowEditDialog { get; set; } = false;
        public bool ShowDeleteConfirmation { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            await BindGrid();
            await BindDropdownValues();
            StateHasChanged();
            await base.OnInitializedAsync();
        }
        private async Task BindGrid()
        {
            if (feesStructureService is not null)
            {
                var response = await feesStructureService.GetAllAsync();
                if (response is not null && response.Success)
                {
                    FeesStructureList = response.Data;
                }
            }
        }
        private async Task BindDropdownValues()
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
                    DropdownFeesHeadList.AddRange(response.Data.Select(x => new SelectListItem(x.Name, x.Id.ToString())));
                }
            }
            //DropdownFixOrPercentageList.Add(new SelectListItem());
            //DropdownFixOrPercentageList.AddRange(Enum.GetValues(typeof(FeesStructureTypeEnum))
            //                                    .Cast<FeesStructureTypeEnum>()
            //                                    .Select(x => new SelectListItem(x.ToString(), ((int)x).ToString())));
        }
        void AddHandler()
        {
            SelectedData = new FeesStructures();
            ShowEditDialog = true;
            StateHasChanged();
        }

        private async void EditHandler(int feesStructureId)
        {
            if (feesStructureService is not null)
            {
                if (feesStructureId > 0)
                {
                    var response = await feesStructureService.GetByIdAsync(feesStructureId);
                    if (response is not null && response.Success)
                    {
                        SelectedData = response.Data;
                    }
                }
                ShowEditDialog = true;
                StateHasChanged();
            }
        }

        private async void FixOrPercentageChangeHandler(object selectedValue)
        {
            SelectedData.IsFixOrPercentage = (FeesStructureTypeEnum)selectedValue;
        }
        void CloseHandler()
        {
            SelectedData = new FeesStructures();
            ShowEditDialog = false;
            StateHasChanged();
        }
        async Task SaveHandler()
        {
            if (feesStructureService is not null)
            {
                APIResult<FeesStructures>? response;
                if (SelectedData.Id > 0)
                    response = await feesStructureService.ModifyByIdAsync(SelectedData.Id, SelectedData);
                else
                    response = await feesStructureService.CreateAsync(SelectedData);

                if (response is not null && response.Success)
                {
                    SelectedData = new FeesStructures();
                    await BindGrid();
                    ShowEditDialog = false;
                    StateHasChanged();
                }
            }
        }
        void DeleteHandler(int feesStructureId)
        {
            SelectedData.Id = feesStructureId;
            ShowDeleteConfirmation = true;
            StateHasChanged();
        }
        void ConfirmDeleteCancelHandler()
        {
            SelectedData = new FeesStructures();
            ShowDeleteConfirmation = false;
            StateHasChanged();
        }
        async Task ConfirmDeleteOkHandler()
        {
            if (feesStructureService is not null)
            {
                if (SelectedData.Id > 0)
                {
                    int feesStructureId = SelectedData.Id;
                    var response = await feesStructureService.DeleteAsync(feesStructureId);
                    if (response is not null && response.Success)
                    {
                        SelectedData = new FeesStructures();
                        ShowDeleteConfirmation = false;
                        await BindGrid();
                        StateHasChanged();
                    }
                }
            }
        }
    }
}
