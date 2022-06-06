using System;
using System.Runtime.Serialization;

namespace Business.Constants
{
    public static class Messages
    {
        //Product
        public static string ProductAdded = "Product Added Successfully";

        public static string ProductDeleted = "Product Successfully Deleted";

        public static string ProductUpdated = "Product Successfully Updated";

        //Category
        public static string CategoryAdded = "Category Added Successfully";

        public static string CategoryDeleted = "Category Added Successfully";

        public static string CategoryUpdated = "Category Added Successfully";

        //User
        public static string UserNotFound = "User not found";

        public static string PasswordError = "Incorrect Password";

        public static string SuccessfullLogin = "Successfully Login";

        public static string UserAlreadyExists = "This User is Registered in the System";

        public static string UserRegistered = "User Registered Successfully";

        public static string AccessTokenCreated = "Access Token Created";

        public static string AuthorizationDenied = "Authorization Denied";
    }
}
