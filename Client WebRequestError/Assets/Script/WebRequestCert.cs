using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class WebRequestCert : UnityEngine.Networking.CertificateHandler
{
    private static string PUB_KEY = "";
    
    protected override bool ValidateCertificate(byte[] certificateData)
    {

        return true;
        
        X509Certificate2 certificate = new X509Certificate2(certificateData);
        string pk = certificate.GetPublicKeyString();
        if (pk.Equals(PUB_KEY))
            return true;

        // Bad dog
        return false;
    }

}