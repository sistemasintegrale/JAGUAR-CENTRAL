namespace SGE.Entity.FacturaElectronica
{
    public class BaseResponse<T>
    {
        public bool Succes { get; set; }
        public T Data { get; set; }
        public string Msg { get; set; }

        public BaseResponse()
        {
            Succes = true;
        }
    }
}
