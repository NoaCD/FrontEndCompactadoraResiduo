﻿namespace FrontEndCompactadoraResiduos.Bussiness.Servicios
{
    public class InvalidarCredencialesSSL
    {

        public HttpClientHandler init()
        {
            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                };
            return handler;
        }

    }
}
