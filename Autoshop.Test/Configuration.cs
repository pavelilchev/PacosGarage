namespace Autoshop.Test
{
    using AutoMapper;
    using Autoshop.Common.Mapping;
    using System;

    public class Configuration
    {
        private static bool testsInitialized = false;

        public static void Initialize()
        {
            if (!testsInitialized)
            {
                try
                {
                    Mapper.Initialize(config => config.AddProfile<AutoMapperProfile>());
                }
                catch (Exception)
                {
                }
                testsInitialized = true;
            }
        }
    }
}
