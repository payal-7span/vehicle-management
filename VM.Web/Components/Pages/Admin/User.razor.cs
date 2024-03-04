using Microsoft.AspNetCore.Components;
using VM.Service.Models;
using VM.Service.Services;

namespace VM.Web.Components.Pages.Admin
{
    public partial class User
    {
        [Inject]
        private IUserService? userService { get; set; }

        public List<Users> UserList { get; set; } = new List<Users>();

        public Users SelectedData { get; set; } = new Users();
        public bool ShowEditDialog { get; set; } = false;
        public bool ShowDeleteConfirmation { get; set; } = false;
        public bool ShowSetPasswordDialog { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            await BindGrid();
            StateHasChanged();
            await base.OnInitializedAsync();
        }

        private async Task BindGrid()
        {
            if (userService is not null)
            {
                var response = await userService.GetAllAsync();
                if (response is not null && response.Success)
                {
                    UserList = response.Data;
                }
            }
        }
        void AddHandler()
        {
            SelectedData = new Users();
            ShowEditDialog = true;
            StateHasChanged();
        }

        private async void EditHandler(int userId)
        {
            if (userService is not null)
            {
                if (userId > 0)
                {
                    var response = await userService.GetByIdAsync(userId);
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
            SelectedData = new Users();
            ShowEditDialog = false;
            StateHasChanged();
        }

        async Task SaveHandler()
        {
            if (userService is not null)
            {
                APIResult<Users>? response;
                if (SelectedData.Id > 0)
                    response = await userService.ModifyByIdAsync(SelectedData.Id, SelectedData);
                else
                    response = await userService.CreateAsync(SelectedData);

                if (response is not null && response.Success)
                {
                    SelectedData = new Users();
                    await BindGrid();
                    ShowEditDialog = false;
                    StateHasChanged();
                }
            }
        }
        void DeleteHandler(int userId)
        {
            SelectedData.Id = userId;
            ShowDeleteConfirmation = true;
            StateHasChanged();
        }
        void ConfirmDeleteCancelHandler()
        {
            SelectedData = new Users();
            ShowDeleteConfirmation = false;
            StateHasChanged();
        }
        async Task ConfirmDeleteOkHandler()
        {
            if (userService is not null)
            {
                if (SelectedData.Id > 0)
                {
                    int userId = SelectedData.Id;
                    var response = await userService.DeleteAsync(userId);
                    if (response is not null && response.Success)
                    {
                        SelectedData = new Users();
                        ShowDeleteConfirmation = false;
                        await BindGrid();
                        StateHasChanged();
                    }
                }
            }
        }

        async Task SetPasswordHandler(int userId)
        {
            if (userService is not null)
            {
                if (userId > 0)
                {
                    var response = await userService.GetByIdAsync(userId);
                    if (response is not null && response.Success)
                    {
                        SelectedData = response.Data;
                    }
                }
                ShowSetPasswordDialog = true;
                StateHasChanged();
            }
        }

        void CloseSetPasswordHandler()
        {
            SelectedData = new Users();
            ShowSetPasswordDialog = false;
            StateHasChanged();
        }

        async Task SaveSetPasswordHandler()
        {
            if (userService is not null)
            {
                int userId = 3;
                if (userId > 0)
                {
                    var response = await userService.SetPasswordAsync(SelectedData);

                    if (response is not null && response.Success)
                    {
                        SelectedData = new Users();
                        ShowSetPasswordDialog = false;
                        StateHasChanged();
                    }
                }
            }
        }
    }
}
