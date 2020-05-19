using System;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace api.Utils
{
    public static class Encriptar
    {
        private static byte[] salt = { 0, 100, 120, 210, 255 };

        public static string GerarSenha(string senha)
        {
            string senhaEncriptada = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: senha,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8
            ));

            return senhaEncriptada;
        }

    }
}
