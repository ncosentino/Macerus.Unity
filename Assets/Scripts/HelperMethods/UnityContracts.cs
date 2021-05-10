using UnityEngine;

namespace NexusLabs.Contracts // common namespace with our other NexusLabs.Contracts
{
    public static class UnityContracts
    {
        public static void RequiresNotNull<T>(
            this Behaviour behaviour,
            T value,
            string name)
        {
            Contract.RequiresNotNull(
                value,
                $"{name} was not set on '{behaviour.transform.gameObject}.{behaviour}'.");
        }

        public static void RequiresNotNullOrWhitespace(
            this Behaviour behaviour,
            string value,
            string name)
        {
            Contract.RequiresNotNullOrWhiteSpace(
                value,
                $"{name} was null or whitespace on '{behaviour.transform.gameObject}.{behaviour}'.");
        }
    }
}
