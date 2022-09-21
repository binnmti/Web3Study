using Microsoft.Extensions.Configuration;
using NftUtility;

namespace NFTImageMAUIViewer
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        string InfuraApiKey;

        public MainPage()
        {
            InitializeComponent();

            var config = new ConfigurationBuilder().AddUserSecrets<MainPage>().Build();
            InfuraApiKey = config["Settings:InfuraApiKey"];
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }

        HttpClient client = new HttpClient();


        private async Task SetImageAsync()
        {
            var url = await ContractAddressEntry.Text.ToImageUrlAsync(40913, InfuraApiKey, client);

            using var imageResponse = await client.GetAsync(url.AbsoluteUri);
            if (!imageResponse.IsSuccessStatusCode) return;

            //画像を保存
            using var stream = await imageResponse.Content.ReadAsStreamAsync();
            IpfsImage.Source = ImageSource.FromStream(() => stream);
        }

        private async void OnWeb3ButtonClicked(object sender, EventArgs e)
        {
            (sender as Button).IsEnabled = false;

            await SetImageAsync();

            (sender as Button).IsEnabled = true;
        }
    }
}