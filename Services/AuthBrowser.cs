using Duende.IdentityModel.OidcClient.Browser;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;

namespace Vibes.Services
{
    public class AuthBrowser : IBrowser
    {
        private readonly string _cachePath;

        public AuthBrowser(string cachePath)
        {
            _cachePath = cachePath;
        }

        public async Task<BrowserResult> InvokeAsync(BrowserOptions options, CancellationToken cancellationToken = default)
        {
            var tcs = new TaskCompletionSource<BrowserResult>();

            var form = new Form
            {
                Text = "Authenticating...",
                Width = 500,
                Height = 650,
                StartPosition = FormStartPosition.CenterScreen,
                MinimizeBox = false,
                MaximizeBox = false
            };

            var webView = new WebView2 { Dock = DockStyle.Fill };
            form.Controls.Add(webView);

            var env = await CoreWebView2Environment.CreateAsync(userDataFolder: _cachePath);
            await webView.EnsureCoreWebView2Async(env);

            webView.NavigationStarting += (s, e) =>
            {
                if (e.Uri.StartsWith(options.EndUrl))
                {
                    tcs.SetResult(new BrowserResult
                    {
                        ResultType = BrowserResultType.Success,
                        Response = e.Uri
                    });
                    form.Close();
                }
            };

            form.FormClosing += (s, e) =>
            {
                if (!tcs.Task.IsCompleted)
                {
                    tcs.SetResult(new BrowserResult { ResultType = BrowserResultType.UserCancel });
                }
            };

            webView.Source = new Uri(options.StartUrl);
            form.Show();

            return await tcs.Task;
        }
    }
}