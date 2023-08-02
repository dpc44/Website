using System;
using System.Collections.Generic;
using System.Text;

namespace Website.Articles
{
    public enum ArticleStatus : byte
    {
        Accepted,
        Denied,
        Cancelled,
        Stop,
        NotAvailable,
    }
}
