# Cryptallet - A cryptocurrency app
Cryptallet is a small application to demonstrate how to work with Ethereum and Ethereum based token with Xamarin.
The app is built with Xamarin.Forms, Prism with modularity structure.

## About
This project is maintained by Naxam Co.,Ltd.<br>
We specialize in developing mobile applications using Xamarin and native technology stack.<br>

**Looking for developers for your project?**<br>

<a href="mailto:tuyen@naxam.net"> 
<img src="https://github.com/NAXAM/naxam.github.io/blob/master/assets/img/hire_button.png?raw=true" height="40"></a> <br>

## Screenshots
| Unlock        | Home           | History  |
| ------------- |----------------| ---------|
|<img src="./screenshots/unlock.png" alt="Unlock"/>| <img src="./screenshots/home.png" alt="Home"/> | <img src="./screenshots/history.png" alt="History"/> |

## How to folk
Currently, the app is worked with my own created ERC20 contract. You might need to change the contract address to your desired one in order to test out the app.

Inside *AccountsManager* class, change line 48.
```
const string CONTRACT_ADDRESS = "{YOUR_SMART_CONTRACT_ADDRESS}";
```

You will also need to point out to your appropriate Ethereum network of your choice.

Inside *AccountsManager* class, change line 131
```
var client = new RpcClient(new Uri("{YOUR_ETHEREUM_NETWORK}"));
```

## License

Cryptallet application is released under the Apache License, Version 2.0.
See [LICENSE](./LICENSE) for details.

# Get our showcases on AppStore/PlayStore
Try our showcases to know more about our capabilities. 

<a href="https://itunes.apple.com/us/developer/tuyen-vu/id1255432728/" > 
<img src="https://github.com/NAXAM/imagepicker-android-binding/raw/master/art/apple_store.png" width="117" height="34"></a>

<a href="https://play.google.com/store/apps/developer?id=NAXAM+CO.,+LTD" > 
<img src="https://github.com/NAXAM/imagepicker-android-binding/raw/master/art/google_store.png" width="117" height="34"></a>

Contact us if interested.

<a href="mailto:tuyen@naxam.net"> 
<img src="https://github.com/NAXAM/naxam.github.io/blob/master/assets/img/hire_button.png" height="34"></a> <br>
<br>

Follow us for the latest updates<br>[![Twitter URL](https://img.shields.io/twitter/url/http/shields.io.svg?style=social)](https://twitter.com/intent/tweet?text=https://github.com/NAXAM/cryptallet-xamarin-forms)
[![Twitter Follow](https://img.shields.io/twitter/follow/naxamco.svg?style=social)](https://twitter.com/naxamco)