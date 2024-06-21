﻿using Raiffeisen.RegistarMjenica.Api.Helpers;

namespace Raiffeisen.RegistarMjenica.Api.Extensions;

public static class StateContainerExtensions
{
    public static int AddRoutingObjectParameter(this StateContainer stateContainer, object value)
    {
        stateContainer.ObjectTunnel[value.GetHashCode()] = value;
        return value.GetHashCode();
    }

    public static T GetRoutingObjectParameter<T>(this StateContainer stateContainer, int hashCode)
    {
        return (T)stateContainer.ObjectTunnel.PopValue(hashCode);
    }
}