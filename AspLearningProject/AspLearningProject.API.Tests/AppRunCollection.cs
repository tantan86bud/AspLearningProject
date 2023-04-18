﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspLearningProject.API.Tests
{
    [CollectionDefinition("App run")]
    public class DatabaseCollection : ICollectionFixture<AppRun>
    {
        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
    }
}
