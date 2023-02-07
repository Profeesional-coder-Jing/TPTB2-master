using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Toolbelt.Blazor;

namespace TPTB2.Client.Services
{
    public class HttpInterceptorService
    {
        private readonly HttpClientInterceptor interceptor;
        private readonly NavigationManager navManager;
        private int responseCode;

        public HttpInterceptorService(HttpClientInterceptor interceptor, NavigationManager navManager)
        {
            this.interceptor = interceptor;
            this.navManager = navManager;
        }

        public object Administrator { get; private set; }

        public void MonitorEvent() => interceptor.AfterSend += InterceptResponse;

        public void DisposeEvent() => interceptor.AfterSend -= InterceptResponse;

        private void InterceptResponse(object sender, HttpClientInterceptorEventArgs e)
        {
            string message = string.Empty;
            if (!e.Response.IsSuccessStatusCode)
            {
                var responseCode = e.Response.StatusCode;
                switch (responseCode)
                {
                    //To be implemented
                }
            }
            switch (responseCode)
            {
                case (int)HttpStatusCode.NotFound:
                    navManager.NavigateTo("/404");
                    message = "The requested resource was not found.";
                    break;
                case (int)HttpStatusCode.Unauthorized:
                case (int)HttpStatusCode.Forbidden:
                    navManager.NavigateTo("/unauthorized");
                    message = "You are not authorized to access this resource. ";
                    break;
                default:
                    navManager.NavigateTo("/500");
                    message = "Something went wrong, please contact Administrator";
                break;
            }
        }
    }
}
