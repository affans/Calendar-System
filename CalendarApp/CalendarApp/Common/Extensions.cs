using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CalendarApp.Common
{
    public static class Extensions
    {
        public static string EncodeTo64(string toEncode)
        {
            byte[] toEncodeAsBytes
                  = System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);
            string returnValue
                  = System.Convert.ToBase64String(toEncodeAsBytes);
            return returnValue;
        }

        public static string DecodeFrom64(string encodedData)
        {
            byte[] encodedDataAsBytes
                = System.Convert.FromBase64String(encodedData);
            string returnValue =
               System.Text.ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);
            return returnValue;
        }
    }
    public enum Purpose
    {
        EditGroupName = 1,
        EditGroupTitle = 2,
        EditType = 3,
        DeleteGroupName = 4,
        DeleteGroupTitle = 5,
        DeleteTYpe = 6,
        AddGroupName = 7,
        AddGroupTitle = 8,
        AddTitleType = 9
    }
}