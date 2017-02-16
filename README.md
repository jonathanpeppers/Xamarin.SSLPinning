# Xamarin.SSLPinning
Test project to setup SSL pinning with Xamarin

Looking for a workaround for blog post [here](https://thomasbandt.com/certificate-and-public-key-pinning-with-xamarin)

Basically I did the following to get this to work:
- Dumped Xamarin's source for `NSUrlSessionHandler.cs` and its code in `HttpClientEx.cs`
- Figured out a quick command to export certificate
- Wrote some C# code ported from Obj-C [here](https://gist.github.com/edwardmp/df8517aa9f1752e73353)

My code:
```csharp
var serverCertChain = challenge.ProtectionSpace.ServerSecTrust;
var first = serverCertChain[0].DerData;
var cert = NSData.FromFile("httpbin.cer");
if (first.IsEqual(cert))
{
    completionHandler(NSUrlSessionAuthChallengeDisposition.PerformDefaultHandling, challenge.ProposedCredential);
}
else
{
    completionHandler(NSUrlSessionAuthChallengeDisposition.RejectProtectionSpace, null);
}
```
*NOTE: you may want to do some checks for empty array & reuse NSData for better performance*
*MORE NOTE: I would not ship the public key as a flat file in your app, place it somewhere safe inside an assembly so an attacker will not easily replace it with their own*

If you need to get the cert for your own site, run the command:
```
openssl s_client -connect httpbin.org:443 | openssl x509 -outform DER > httpbin.cer
```
Replace `httpbin` with your domain.

If Xamarin could somehow expose `DidReceiveChallenge` that would be awesome!
