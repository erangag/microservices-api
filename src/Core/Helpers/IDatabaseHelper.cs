﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Helpers
{
    public interface IDatabaseHelper
    {
        SqlConnection CreateAndOpenConnection();
    }
}
