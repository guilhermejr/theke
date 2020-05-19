using api.Utils;

namespace api.Models
{
    public class ModeloPadrao
    {

        public override string ToString()
        {
            return RetornaJson.Retornar(this);
        }

    }
}
