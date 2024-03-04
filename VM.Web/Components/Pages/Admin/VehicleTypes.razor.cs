using Microsoft.AspNetCore.Components;
using VM.Service.Models;
using VM.Service.Services;

namespace VM.Web.Components.Pages.Admin
{
    public partial class VehicleTypes
    {
        [Inject]
        private IVehicleTypeService? vehicleTypeService { get; set;  }

        public List<VehicleType> VehicleTypeList { get; set; } = new List<VehicleType>();

        public VehicleType SelectedData { get; set; } = new VehicleType();
        public bool ShowEditDialog { get; set; } = false;
        public bool ShowDeleteConfirmation { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            await BindGrid();
            StateHasChanged();
            await base.OnInitializedAsync();
        }

        private async Task BindGrid()
        {
            if (vehicleTypeService is not null)
            {
                var response = await vehicleTypeService.GetAllAsync();
                if (response is not null && response.Success)
                {
                    VehicleTypeList = response.Data;
                }
            }
        }
        void AddHandler()
        {
            SelectedData = new VehicleType();
            ShowEditDialog = true;
            StateHasChanged();
        }

        private async void EditHandler(int vehicleTypeId)
        {
            if (vehicleTypeService is not null)
            {
                if (vehicleTypeId > 0)
                {
                    var response = await vehicleTypeService.GetByIdAsync(vehicleTypeId);
                    if (response is not null && response.Success)
                    {
                        SelectedData = response.Data;
                    }
                }
                ShowEditDialog = true;
                StateHasChanged();
            }
        }

        void CloseHandler()
        {
            SelectedData = new VehicleType();
            ShowEditDialog = false;
            StateHasChanged();
        }

        async Task SaveHandler()
        {
            if (vehicleTypeService is not null)
            {
                APIResult<VehicleType>? response;
                if (SelectedData.Id > 0)
                    response = await vehicleTypeService.ModifyByIdAsync(SelectedData.Id, SelectedData);
                else
                    response = await vehicleTypeService.CreateAsync(SelectedData);

                if (response is not null && response.Success)
                {
                    SelectedData = new VehicleType();
                    await BindGrid();
                    ShowEditDialog = false;
                    StateHasChanged();
                }
            }
        }
        void DeleteHandler(int vehicleTypeId)
        {
            SelectedData.Id = vehicleTypeId;
            ShowDeleteConfirmation = true;
            StateHasChanged();
        }
        void ConfirmDeleteCancelHandler()
        {
            SelectedData = new VehicleType();
            ShowDeleteConfirmation = false;
            StateHasChanged();
        }
        async Task ConfirmDeleteOkHandler()
        {
            if (vehicleTypeService is not null)
            {
                if (SelectedData.Id > 0)
                {
                    int vehicleTypeId = SelectedData.Id;
                    var response = await vehicleTypeService.DeleteAsync(vehicleTypeId);
                    if (response is not null && response.Success)
                    {
                        SelectedData = new VehicleType();
                        ShowDeleteConfirmation = false;
                        await BindGrid();
                        StateHasChanged();
                    }
                }
            }
        }
    }
}
