using Hangfire;
using Reporting.PhoneContact;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Reporting.RecurringJobs
{
    public static class RecurringJobProcessor
    {
        public static void ProcessReports(this IRecurringJobManager recurringJobManager)
        {
            var operation = new PhoneContactOperations();
            operation.GetPhoneContacts();
        }
    }
}
