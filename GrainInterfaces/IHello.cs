﻿namespace GrainInterfaces
{
    using System.Threading.Tasks;

    public interface IHello : Orleans.IGrainWithIntegerKey
    {
        Task<string> SayHello(string greeting);
    }
}
