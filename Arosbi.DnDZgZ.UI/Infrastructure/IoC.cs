namespace Arosbi.DnDZgZ.UI.Infrastructure
{
    using System;
    using System.Collections.Generic;

    public static partial class IoC
    {
        private static readonly Dictionary<Type, Type> _registration = new Dictionary<Type, Type>();
        private static readonly Dictionary<Type, object> _rot = new Dictionary<Type, object>();
        private static readonly object[] _emptyArguments = new object[0];
        private static readonly object _syncLock = new object();

        static partial void RegisterAll();

        static IoC()
        {
            RegisterAll();
        }

        public static object Resolve(Type type)
        {
            lock (_syncLock)
            {
                if (!_rot.ContainsKey(type))
                {
                    if (!_registration.ContainsKey(type))
                    {
                        throw new Exception("Type not registered.");
                    }
                    var resolveTo = _registration[type] ?? type;
                    var constructorInfos = resolveTo.GetConstructors();
                    if (constructorInfos.Length > 1)
                        throw new Exception("Cannot resolve a type that has more than one constructor.");
                    var constructor = constructorInfos[0];
                    var parameterInfos = constructor.GetParameters();
                    if (parameterInfos.Length == 0)
                    {
                        _rot[type] = constructor.Invoke(_emptyArguments);
                    }
                    else
                    {
                        var parameters = new object[parameterInfos.Length];
                        foreach (var parameterInfo in parameterInfos)
                        {
                            parameters[parameterInfo.Position] = Resolve(parameterInfo.ParameterType);
                        }
                        _rot[type] = constructor.Invoke(parameters);
                    }
                }
                return _rot[type];
            }
        }

        public static T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }

        public static void Register<I, C>() where C : class, I
        {
            lock (_syncLock)
            {
                _registration.Add(typeof(I), typeof(C));
            }
        }

        public static void Register<C>() where C : class
        {
            lock (_syncLock)
            {
                _registration.Add(typeof(C), null);
            }
        }
    }
}