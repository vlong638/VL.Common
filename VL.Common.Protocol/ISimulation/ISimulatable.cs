using System;

namespace VL.Common.Protocol.ISimulation
{
    public interface ISimulatable<T>
    {
        bool SimulateCreate(T t, DateTime simulateTime);
    }
}
