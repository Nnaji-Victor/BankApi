using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace BankApi
{
    public static class SecuredPasswordHasher
    {
        public static bool verify(string savedPasswordHash, string password)
        {
            byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);
            //take the salt out of the string
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            //hash the user inputted PW with the salt
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            //put the damn thing in a byte vector.. instead of a string. why? why is this necessary?
            //who am i to judge cryptography standards i guess
            byte[] hash = pbkdf2.GetBytes(20);
            //oh, this is why
            //compare results! letter by letter!
            //starting from 17 cause 0-16 are the salt
            int ok = 1;
            for (int i = 0; i < 20; i++)
                if (hashBytes[i + 16] != hash[i])
                    ok = 0;
            if (ok == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string hashPassword(string password)
        {
            byte[] salt;
            /*generate salt*/
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            /*hash and salt it using PBKDF2*/
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            //place the string in the byte array (thats waht getbytes does)
            byte[] hash = pbkdf2.GetBytes(20);
            //make new byte array where to store the hashed password+salt
            //why 36? cause 20 are for the hash and 16 for the salt
            byte[] hashBytes = new byte[36];
            //place the god damn things where they belong
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            //now, convert our fancy byte array to a string so i can put it in the db
            string savedPasswordHash = Convert.ToBase64String(hashBytes);

            return savedPasswordHash;
        }
    }
}
