using Microsoft.AspNetCore.Components;
using VM.Service.Models;
using VM.Service.Services;

namespace VM.Web.Components.Pages.Admin
{
    public partial class FeesHead
    {
        [Inject]
        private IFeesHeadService? feesHeadService { get; set; }

        public List<FeesHeads> FeesHeadList { get; set; } = new List<FeesHeads>();

        public FeesHeads SelectedData { get; set; } = new FeesHeads();
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
            if (feesHeadService is not null)
            {
                var response = await feesHeadService.GetAllAsync();
                if (response is not null && response.Success)
                {
                    FeesHeadList = response.Data;
                }
            }
        }
        void AddHandler()
        {
            SelectedData = new FeesHeads();
            ShowEditDialog = true;
            StateHasChanged();
        }

        private async void EditHandler(int feesHeadId)
        {
            if (feesHeadService is not null)
            {
                if (feesHeadId > 0)
                {
                    var response = await feesHeadService.GetByIdAsync(feesHeadId);
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
            SelectedData = new FeesHeads();
            ShowEditDialog = false;
            StateHasChanged();
        }
        async Task SaveHandler()
        {
            if (feesHeadService is not null)
            {
                APIResult<FeesHeads>? response;
                if (SelectedData.Id > 0)
                    response = await feesHeadService.ModifyByIdAsync(SelectedData.Id, SelectedData);
                else
                    response = await feesHeadService.CreateAsync(SelectedData);

                if (response is not null && response.Success)
                {
                    SelectedData = new FeesHeads();
                    await BindGrid();
                    ShowEditDialog = false;
                    StateHasChanged();
                }
            }
        }
        void DeleteHandler(int feesHeadId)
        {
            SelectedData.Id = feesHeadId;
            ShowDeleteConfirmation = true;
            StateHasChanged();
        }
        void ConfirmDeleteCancelHandler()
        {
            SelectedData = new FeesHeads();
            ShowDeleteConfirmation = false;
            StateHasChanged();
        }
        async Task ConfirmDeleteOkHandler()
        {
            if (feesHeadService is not null)
            {
                if (SelectedData.Id > 0)
                {
                    int feesHeadId = SelectedData.Id;
                    var response = await feesHeadService.DeleteAsync(feesHeadId);
                    if (response is not null && response.Success)
                    {
                        SelectedData = new FeesHeads();
                        ShowDeleteConfirmation = false;
                        await BindGrid();
                        StateHasChanged();
                    }
                }
            }
        }
    }
}
