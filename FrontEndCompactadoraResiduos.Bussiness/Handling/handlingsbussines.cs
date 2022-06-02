namespace CreativeReduction.Bussiness.Handling
{
    public class handlingsbussines
    {

        public HttpClientHandler hanlingbusines()
        {

            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (HttpRequestMessage, cert, cetChain, PolicyErrors) =>
                {
                    return true;
                };

            return handler;
        }

    }
}
