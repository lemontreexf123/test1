﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace App3
{
    public abstract class PermissionUtil
    {
        public static bool VerifyPermissions(Permission[] grantResults)
        {
            // At least one result must be checked.
            if (grantResults.Length < 1)
                return false;

            // Verify that each required permission has been granted, otherwise return false.
            foreach (Permission result in grantResults)
            {
                if (result != Permission.Granted)
                {
                    return false;
                }
            }
            return true;
        }
    }
}