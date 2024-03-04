using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using VM.Service.Models;
using VM.Service.Services;

namespace VM.Web.Components.Pages.Auth
{
    public partial class SignIn
    {

        [Inject]
        private IAuthService? authService { get; set; }

        public Login LogindData { get; set; } = new Login();
        [Inject]
        private ILocalStorageService _localStorageService { get; set; }

        [Inject]
        private NavigationManager navigationManager { get; set; }
        protected override async Task OnInitializedAsync()
        {
            StateHasChanged();
            await base.OnInitializedAsync();
        }

        async Task LoginHandler()
        {
            if (LogindData.Email != null && LogindData.Password != null)
            {
                if (authService is not null)
                {
                    APIResult<LoginResult>? response;

                    response = await authService.LoginAsync(LogindData);

                    if (response is not null && response.Success)
                    {
                        await _localStorageService.SetItemAsStringAsync("token", response.Data.token);
                        navigationManager.NavigateTo("/");
                    }
                }
            }
            StateHasChanged();
        }

    }
}
