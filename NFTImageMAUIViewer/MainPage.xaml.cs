using Microsoft.Extensions.Configuration;
using Nethereum.Web3;
using System.Net.Http.Json;
using System.Numerics;
using System.Text.Json;

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




        public class Rootobject
        {
            public string title { get; set; }
            public string name { get; set; }
            public string type { get; set; }
            public string imageUrl { get; set; }
            public string description { get; set; }
            public Attribute[] attributes { get; set; }
            public Properties properties { get; set; }
        }

        public class Properties
        {
            public Name name { get; set; }
            public Description description { get; set; }
            public Preview_Media_File preview_media_file { get; set; }
            public Preview_Media_File_Type preview_media_file_type { get; set; }
            public Created_At created_at { get; set; }
            public Total_Supply total_supply { get; set; }
            public Digital_Media_Signature_Type digital_media_signature_type { get; set; }
            public Digital_Media_Signature digital_media_signature { get; set; }
            public Raw_Media_File raw_media_file { get; set; }
        }

        public class Name
        {
            public string type { get; set; }
            public string description { get; set; }
        }

        public class Description
        {
            public string type { get; set; }
            public string description { get; set; }
        }

        public class Preview_Media_File
        {
            public string type { get; set; }
            public string description { get; set; }
        }

        public class Preview_Media_File_Type
        {
            public string type { get; set; }
            public string description { get; set; }
        }

        public class Created_At
        {
            public string type { get; set; }
            public DateTime description { get; set; }
        }

        public class Total_Supply
        {
            public string type { get; set; }
            public int description { get; set; }
        }

        public class Digital_Media_Signature_Type
        {
            public string type { get; set; }
            public string description { get; set; }
        }

        public class Digital_Media_Signature
        {
            public string type { get; set; }
            public string description { get; set; }
        }

        public class Raw_Media_File
        {
            public string type { get; set; }
            public string description { get; set; }
        }

        public class Attribute
        {
            public string trait_type { get; set; }
            public string value { get; set; }
        }

        HttpClient client = new HttpClient();


        private async Task SetImageAsync()
        {
            var web3 = new Web3($"https://mainnet.infura.io/v3/{InfuraApiKey}");
            //おまじない？
            var abi = @"[{""inputs"":[],""stateMutability"":""nonpayable"",""type"":""constructor""},{""anonymous"":false,""inputs"":[{""indexed"":true,""internalType"":""address"",""name"":""owner"",""type"":""address""},{""indexed"":true,""internalType"":""address"",""name"":""approved"",""type"":""address""},{""indexed"":true,""internalType"":""uint256"",""name"":""tokenId"",""type"":""uint256""}],""name"":""Approval"",""type"":""event""},{""anonymous"":false,""inputs"":[{""indexed"":true,""internalType"":""address"",""name"":""owner"",""type"":""address""},{""indexed"":true,""internalType"":""address"",""name"":""operator"",""type"":""address""},{""indexed"":false,""internalType"":""bool"",""name"":""approved"",""type"":""bool""}],""name"":""ApprovalForAll"",""type"":""event""},{""anonymous"":false,""inputs"":[{""indexed"":true,""internalType"":""address"",""name"":""from"",""type"":""address""},{""indexed"":true,""internalType"":""address"",""name"":""to"",""type"":""address""},{""indexed"":true,""internalType"":""uint256"",""name"":""tokenId"",""type"":""uint256""}],""name"":""Transfer"",""type"":""event""},{""inputs"":[{""internalType"":""address"",""name"":""to"",""type"":""address""},{""internalType"":""uint256"",""name"":""tokenId"",""type"":""uint256""}],""name"":""approve"",""outputs"":[],""stateMutability"":""nonpayable"",""type"":""function""},{""inputs"":[{""internalType"":""address"",""name"":""owner"",""type"":""address""}],""name"":""balanceOf"",""outputs"":[{""internalType"":""uint256"",""name"":"""",""type"":""uint256""}],""stateMutability"":""view"",""type"":""function""},{""inputs"":[{""internalType"":""address"",""name"":""_newOwner"",""type"":""address""}],""name"":""changeOwner"",""outputs"":[{""internalType"":""bool"",""name"":"""",""type"":""bool""}],""stateMutability"":""nonpayable"",""type"":""function""},{""inputs"":[{""internalType"":""address"",""name"":""_itemOwner"",""type"":""address""},{""internalType"":""uint256"",""name"":""_id"",""type"":""uint256""},{""internalType"":""string"",""name"":""_tokenURI"",""type"":""string""}],""name"":""forgeNewNFT"",""outputs"":[{""internalType"":""bool"",""name"":"""",""type"":""bool""}],""stateMutability"":""nonpayable"",""type"":""function""},{""inputs"":[{""internalType"":""uint256"",""name"":""tokenId"",""type"":""uint256""}],""name"":""getApproved"",""outputs"":[{""internalType"":""address"",""name"":"""",""type"":""address""}],""stateMutability"":""view"",""type"":""function""},{""inputs"":[{""internalType"":""address"",""name"":""owner"",""type"":""address""},{""internalType"":""address"",""name"":""operator"",""type"":""address""}],""name"":""isApprovedForAll"",""outputs"":[{""internalType"":""bool"",""name"":"""",""type"":""bool""}],""stateMutability"":""view"",""type"":""function""},{""inputs"":[],""name"":""name"",""outputs"":[{""internalType"":""string"",""name"":"""",""type"":""string""}],""stateMutability"":""view"",""type"":""function""},{""inputs"":[{""internalType"":""uint256"",""name"":""tokenId"",""type"":""uint256""}],""name"":""ownerOf"",""outputs"":[{""internalType"":""address"",""name"":"""",""type"":""address""}],""stateMutability"":""view"",""type"":""function""},{""inputs"":[{""internalType"":""address"",""name"":""from"",""type"":""address""},{""internalType"":""address"",""name"":""to"",""type"":""address""},{""internalType"":""uint256"",""name"":""tokenId"",""type"":""uint256""}],""name"":""safeTransferFrom"",""outputs"":[],""stateMutability"":""nonpayable"",""type"":""function""},{""inputs"":[{""internalType"":""address"",""name"":""from"",""type"":""address""},{""internalType"":""address"",""name"":""to"",""type"":""address""},{""internalType"":""uint256"",""name"":""tokenId"",""type"":""uint256""},{""internalType"":""bytes"",""name"":""_data"",""type"":""bytes""}],""name"":""safeTransferFrom"",""outputs"":[],""stateMutability"":""nonpayable"",""type"":""function""},{""inputs"":[{""internalType"":""address"",""name"":""operator"",""type"":""address""},{""internalType"":""bool"",""name"":""approved"",""type"":""bool""}],""name"":""setApprovalForAll"",""outputs"":[],""stateMutability"":""nonpayable"",""type"":""function""},{""inputs"":[{""internalType"":""bytes4"",""name"":""interfaceId"",""type"":""bytes4""}],""name"":""supportsInterface"",""outputs"":[{""internalType"":""bool"",""name"":"""",""type"":""bool""}],""stateMutability"":""view"",""type"":""function""},{""inputs"":[],""name"":""symbol"",""outputs"":[{""internalType"":""string"",""name"":"""",""type"":""string""}],""stateMutability"":""view"",""type"":""function""},{""inputs"":[{""internalType"":""uint256"",""name"":""tokenId"",""type"":""uint256""}],""name"":""tokenURI"",""outputs"":[{""internalType"":""string"",""name"":"""",""type"":""string""}],""stateMutability"":""view"",""type"":""function""},{""inputs"":[{""internalType"":""address"",""name"":""from"",""type"":""address""},{""internalType"":""address"",""name"":""to"",""type"":""address""},{""internalType"":""uint256"",""name"":""tokenId"",""type"":""uint256""}],""name"":""transferFrom"",""outputs"":[],""stateMutability"":""nonpayable"",""type"":""function""}]";

            //これは指定する
            var contract = web3.Eth.GetContract(abi, ContractAddressEntry.Text);
            var tokenFunction = contract.GetFunction("tokenURI");
            BigInteger myToken = 1;

            //ここも良くわからない。
            return;


            var myResult = await tokenFunction.CallAsync<string>(myToken);


            var ipfs = myResult.Replace("ipfs://ipfs/", "https://ipfs.io/ipfs/");
            var response = await client.GetAsync(ipfs);
            if (!response.IsSuccessStatusCode) return;

            var prefecturesJsonString = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<Rootobject>(prefecturesJsonString);
            using var imageResponse = await client.GetAsync(result.imageUrl);
            if (!imageResponse.IsSuccessStatusCode) return;

            //画像を保存
            using var stream = await imageResponse.Content.ReadAsStreamAsync();
            IpfsImage.Source = ImageSource.FromStream(() => stream);
        }

        private async void OnWeb3ButtonClicked(object sender, EventArgs e)
        {
            (sender as Button).IsEnabled = false;

            var response = await client.GetAsync("https://ipfs.io/ipfs");


            //await SetImageAsync();
            
            (sender as Button).IsEnabled = true;
        }
    }
}