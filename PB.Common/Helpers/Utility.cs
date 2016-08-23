
using System;
using System.Data.SqlClient;
using System.Drawing;
//using System.Drawing.Imaging;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

//using System.Web.Mvc;
//using System.Web.Script.Serialization;

namespace PB.Common.Helpers
{
    public static class Utility
    {
        //public static string ConvertJsonToString(JsonResult json)
        //{
        //    JavaScriptSerializer serializer = new JavaScriptSerializer();

        //    serializer.MaxJsonLength = Int32.MaxValue;
        //    string stringified = serializer.Serialize(json.Data);

        //    return stringified;
        //}

        //public static Image FixedSize(byte[] contents, int Width, int Height)
        //{
        //    Image imgPhoto = byteArrayToImage(contents);

        //    int sourceWidth = imgPhoto.Width;
        //    int sourceHeight = imgPhoto.Height;
        //    int destX = 0;
        //    int destY = 0;

        //    float nPercent = 0;
        //    float nPercentW = 0;
        //    float nPercentH = 0;

        //    nPercentW = ((float)Width / (float)sourceWidth);
        //    nPercentH = ((float)Height / (float)sourceHeight);

        //    if (nPercentH < nPercentW)
        //    {
        //        nPercent = nPercentH;
        //        destX = System.Convert.ToInt16((Width -
        //                      (sourceWidth * nPercent)) / 2);
        //    }
        //    else
        //    {
        //        nPercent = nPercentW;
        //        destY = System.Convert.ToInt16((Height -
        //                      (sourceHeight * nPercent)) / 2);
        //    }

        //    int destWidth = (int)(sourceWidth * nPercent);
        //    int destHeight = (int)(sourceHeight * nPercent);

        //    Bitmap bmPhoto = new Bitmap(Width, Height,
        //                      PixelFormat.Format24bppRgb);
        //    bmPhoto.SetResolution(imgPhoto.HorizontalResolution,
        //                     imgPhoto.VerticalResolution);

        //    return bmPhoto;
        //}

        //public static byte[] imageToByteArray(System.Drawing.Image imageIn)
        //{
        //    MemoryStream ms = new MemoryStream();
        //    imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
        //    return ms.ToArray();
        //}

        //public static Image byteArrayToImage(byte[] byteArrayIn)
        //{
        //    MemoryStream ms = new MemoryStream(byteArrayIn);
        //    System.Drawing.Image returnImage = Image.FromStream(ms);
        //    return returnImage;
        //}

        public static string Encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        public static string Decrypt(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText))
                return string.Empty;

            string EncryptionKey = "MAKV2SPBNI99212";
            //string pattern = "^([A-Za-z0-9+/]{4})*([A-Za-z0-9+/]{4}|[A-Za-z0-9+/]{3}=|[A-Za-z0-9+/]{2}==)$";

            bool isMatch = true;// Regex.IsMatch(cipherText, pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            //------------------------------------------------------------------------------------------------------------------------------
            // JASON STAUFFER (3-31-2015):
            //      I changed this because the above regex was matching 4 character passwords (such as "demo") and was trying to
            //      decrypt it.  That was throwing an unhandled exception.  After checking the interwebs for a regex that would
            //      reliably match AES encrypted strings, I decided to follow advice I found and just try decrypting everything
            //      and catch the exception.  If an exception occurrs, it just skips over that part and returns the original string.
            //
            //------------------------------------------------------------------------------------------------------------------------------

            if (isMatch)
            {
                try
                {
                    byte[] cipherBytes = Convert.FromBase64String(cipherText);
                    using (Aes encryptor = Aes.Create())
                    {
                        Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                        encryptor.Key = pdb.GetBytes(32);
                        encryptor.IV = pdb.GetBytes(16);
                        using (MemoryStream ms = new MemoryStream())
                        {
                            using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                            {
                                cs.Write(cipherBytes, 0, cipherBytes.Length);
                                cs.Close();
                            }
                            cipherText = Encoding.Unicode.GetString(ms.ToArray());
                        }
                    }
                }
                catch { }
            }

            return cipherText;

        }

        public static string IntSuffix(int i)
        {
            string Return = "th";

            switch (i)
            {
                case 1:
                case 21:
                case 31:
                    Return = "st";
                    break;
                case 2:
                case 22:
                    Return = "nd";
                    break;
                case 3:
                    Return = "rd";
                    break;
            }

            return Return;
        }

        public static void RefreshDemo(string email, string constring)
        {
            using (SqlConnection con = new SqlConnection(constring))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand("PB_RefreshDemo", con))
                {
                    com.CommandType = System.Data.CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("email", email);
                    com.ExecuteNonQuery();
                }
                if (con.State != System.Data.ConnectionState.Closed) con.Close();
            }
        }

        public static string DayOfWeek(int dow)
        {
            switch (dow)
            {
                case 0: return "Sunday";
                case 1: return "Monday";
                case 2: return "Tuesday";
                case 3: return "Wednesday";
                case 4: return "Thursday";
                case 5: return "Friday";
                case 6: return "Saturday";
                default: return "";
            }
        }
    }

    public static class StringExtensions
    {
        /// <summary>
        /// Parses the domain from a URL string or returns the string if no URL was found
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string AsDomain(this string url)
        {
            if (string.IsNullOrEmpty(url))
                return url;

            var match = Regex.Match(url, @"^http[s]?[:/]+[^/]+");
            if (match.Success)
                return match.Captures[0].Value;
            else
                return url;
        }

        /// <summary>
        /// Parses the domain from a URL
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string AsDomain(this Uri url)
        {
            if (url == null)
                return null;

            return url.ToString().AsDomain();
        }

        public static string RemoveScheme(this string url)
        {
            var match = Regex.Match(url, @"/(?:https?:\/\/)?(?:www\.)?(.*)\/?$/i");

            if (match.Success)
                return match.Captures[0].Value;
            else
                return url;
        }

        public static string GetAuthority(this string url)
        {
            string regexPattern = @"^(?<s1>(?<s0>[^:/\?#]+):)?(?<a1>"
               + @"//(?<a0>[^/\?#]*))?(?<p0>[^\?#]*)"
               + @"(?<q1>\?(?<q0>[^#]*))?"
               + @"(?<f1>#(?<f0>.*))?";

            Regex re = new Regex(regexPattern, RegexOptions.ExplicitCapture);
            Match m = re.Match(url);

            return m.Groups["a0"].Value;

            //lblOutput.Text +=
            //   m.Groups["s0"].Value + "  (Scheme without colon)<br>";
            //lblOutput.Text +=
            //   m.Groups["s1"].Value + "  (Scheme with colon)<br>";
            //lblOutput.Text +=
            //   m.Groups["a0"].Value + "  (Authority without //)<br>";
            //lblOutput.Text +=
            //   m.Groups["a1"].Value + "  (Authority with //)<br>";
            //lblOutput.Text +=
            //   m.Groups["p0"].Value + "  (Path)<br>";
            //lblOutput.Text +=
            //   m.Groups["q0"].Value + "  (Query without ?)<br>";
            //lblOutput.Text +=
            //   m.Groups["q1"].Value + "  (Query with ?)<br>";
            //lblOutput.Text +=
            //   m.Groups["f0"].Value + "  (Fragment without #)<br>";
            //lblOutput.Text +=
            //   m.Groups["f1"].Value + "  (Fragment with #)<br>";
        }
    }
}

