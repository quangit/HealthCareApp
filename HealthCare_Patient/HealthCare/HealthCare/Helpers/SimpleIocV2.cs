using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading;
using GalaSoft.MvvmLight.Ioc;
using HealthCare.ViewModels;
using Microsoft.Practices.ServiceLocation;

namespace HealthCare.Helpers
{

    #region SimpleIocV2

    public class SimpleIocV2 : ISimpleIoc, IServiceLocator, IServiceProvider
    {
        private static SimpleIocV2 _default;
        private readonly Dictionary<Type, ConstructorInfo> _constructorInfos = new Dictionary<Type, ConstructorInfo>();
        private readonly string _defaultKey = Guid.NewGuid().ToString();
        private readonly object[] _emptyArguments = new object[0];

        private readonly Dictionary<Type, Dictionary<string, Delegate>> _factories =
            new Dictionary<Type, Dictionary<string, Delegate>>();

        private readonly Dictionary<Type, Dictionary<string, object>> _instancesRegistry =
            new Dictionary<Type, Dictionary<string, object>>();

        private readonly Dictionary<Type, Type> _interfaceToClassMap = new Dictionary<Type, Type>();
        private readonly object _syncLock = new object();

        public static SimpleIocV2 Default
        {
            get
            {
                if (_default == null)
                {
                    _default = new SimpleIocV2();
                    RegisterService();
                }
                return _default ?? new SimpleIocV2();
            }
        }

        public bool ContainsCreated<TClass>()
        {
            return ContainsCreated<TClass>(null);
        }

        public bool ContainsCreated<TClass>(string key)
        {
            var key1 = typeof (TClass);
            if (!_instancesRegistry.ContainsKey(key1))
                return false;
            if (string.IsNullOrEmpty(key))
                return _instancesRegistry[key1].Count > 0;
            return _instancesRegistry[key1].ContainsKey(key);
        }

        public bool IsRegistered<T>()
        {
            return _interfaceToClassMap.ContainsKey(typeof (T));
        }

        public bool IsRegistered<T>(string key)
        {
            var key1 = typeof (T);
            if (!_interfaceToClassMap.ContainsKey(key1) || !_factories.ContainsKey(key1))
                return false;
            return _factories[key1].ContainsKey(key);
        }

        public void Register<TInterface, TClass>() where TInterface : class where TClass : class
        {
            Register<TInterface, TClass>(false);
        }

        public void Register<TInterface, TClass>(bool createInstanceImmediately) where TInterface : class
            where TClass : class
        {
            var lockTaken = false;
            object obj = null;
            try
            {
                Monitor.Enter(obj = _syncLock, ref lockTaken);
                var index = typeof (TInterface);
                var type = typeof (TClass);
                if (_interfaceToClassMap.ContainsKey(index))
                {
                    if (_interfaceToClassMap[index] != type)
                        throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture,
                            "There is already a class registered for {0}.", (object) index.FullName));
                }
                else
                {
                    _interfaceToClassMap.Add(index, type);
                    _constructorInfos.Add(type, GetConstructorInfo(type));
                }
                Func<TInterface> factory = MakeInstance<TInterface>;
                DoRegister(index, factory, _defaultKey);
                if (!createInstanceImmediately)
                    return;
                GetInstance<TInterface>();
            }
            finally
            {
                if (lockTaken)
                    Monitor.Exit(obj);
            }
        }

        public void Register<TClass>() where TClass : class
        {
            Register<TClass>(false);
        }

        public void Register<TClass>(bool createInstanceImmediately) where TClass : class
        {
            var index = typeof (TClass);
            if (index.GetTypeInfo().IsInterface)
                throw new ArgumentException("An interface cannot be registered alone.");
            var lockTaken = false;
            object obj = null;
            try
            {
                Monitor.Enter(obj = _syncLock, ref lockTaken);
                if (_factories.ContainsKey(index) && _factories[index].ContainsKey(_defaultKey))
                {
                    if (!_constructorInfos.ContainsKey(index))
                        throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture,
                            "Class {0} is already registered.", (object) index));
                }
                else
                {
                    if (!_interfaceToClassMap.ContainsKey(index))
                        _interfaceToClassMap.Add(index, null);
                    _constructorInfos.Add(index, GetConstructorInfo(index));
                    Func<TClass> factory = MakeInstance<TClass>;
                    DoRegister(index, factory, _defaultKey);
                    if (!createInstanceImmediately)
                        return;
                    GetInstance<TClass>();
                }
            }
            finally
            {
                if (lockTaken)
                    Monitor.Exit(obj);
            }
        }

        public void Register<TClass>(Func<TClass> factory) where TClass : class
        {
            Register(factory, false);
        }

        public void Register<TClass>(Func<TClass> factory, bool createInstanceImmediately) where TClass : class
        {
            if (factory == null)
                throw new ArgumentNullException("factory");
            var lockTaken = false;
            object obj = null;
            try
            {
                Monitor.Enter(obj = _syncLock, ref lockTaken);
                var index = typeof (TClass);
                if (_factories.ContainsKey(index) && _factories[index].ContainsKey(_defaultKey))
                    throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture,
                        "There is already a factory registered for {0}.", (object) index.FullName));
                if (!_interfaceToClassMap.ContainsKey(index))
                    _interfaceToClassMap.Add(index, null);
                DoRegister(index, factory, _defaultKey);
                if (!createInstanceImmediately)
                    return;
                GetInstance<TClass>();
            }
            finally
            {
                if (lockTaken)
                    Monitor.Exit(obj);
            }
        }

        public void Register<TClass>(Func<TClass> factory, string key) where TClass : class
        {
            Register(factory, key, false);
        }

        public void Register<TClass>(Func<TClass> factory, string key, bool createInstanceImmediately)
            where TClass : class
        {
            var lockTaken = false;
            object obj = null;
            try
            {
                Monitor.Enter(obj = _syncLock, ref lockTaken);
                var index = typeof (TClass);
                if (_factories.ContainsKey(index) && _factories[index].ContainsKey(key))
                    throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture,
                        "There is already a factory registered for {0} with key {1}.", (object) index.FullName,
                        (object) key));
                if (!_interfaceToClassMap.ContainsKey(index))
                    _interfaceToClassMap.Add(index, null);
                DoRegister(index, factory, key);
                if (!createInstanceImmediately)
                    return;
                GetInstance<TClass>(key);
            }
            finally
            {
                if (lockTaken)
                    Monitor.Exit(obj);
            }
        }

        public void Reset()
        {
            _interfaceToClassMap.Clear();
            _instancesRegistry.Clear();
            _constructorInfos.Clear();
            _factories.Clear();
        }

        public void Unregister<TClass>() where TClass : class
        {
            var lockTaken = false;
            object obj = null;
            try
            {
                Monitor.Enter(obj = _syncLock, ref lockTaken);
                var key1 = typeof (TClass);
                var key2 = !_interfaceToClassMap.ContainsKey(key1) ? key1 : _interfaceToClassMap[key1] ?? key1;
                if (_instancesRegistry.ContainsKey(key1))
                    _instancesRegistry.Remove(key1);
                if (_interfaceToClassMap.ContainsKey(key1))
                    _interfaceToClassMap.Remove(key1);
                if (_factories.ContainsKey(key1))
                    _factories.Remove(key1);
                if (!_constructorInfos.ContainsKey(key2))
                    return;
                _constructorInfos.Remove(key2);
            }
            finally
            {
                if (lockTaken)
                    Monitor.Exit(obj);
            }
        }

        public void Unregister<TClass>(TClass instance) where TClass : class
        {
            var lockTaken = false;
            object obj = null;
            try
            {
                Monitor.Enter(obj = _syncLock, ref lockTaken);
                var key1 = typeof (TClass);
                if (!_instancesRegistry.ContainsKey(key1))
                    return;
                var dictionary = _instancesRegistry[key1];
                var list = dictionary.Where(pair => pair.Value == (object) instance).ToList();
                for (var index = 0; index < list.Count(); ++index)
                {
                    var key2 = list[index].Key;
                    dictionary.Remove(key2);
                }
            }
            finally
            {
                if (lockTaken)
                    Monitor.Exit(obj);
            }
        }

        public void Unregister<TClass>(string key) where TClass : class
        {
            var lockTaken = false;
            object obj = null;
            try
            {
                Monitor.Enter(obj = _syncLock, ref lockTaken);
                var key1 = typeof (TClass);
                if (_instancesRegistry.ContainsKey(key1))
                {
                    var dictionary = _instancesRegistry[key1];
                    var list = dictionary.Where(pair => pair.Key == key).ToList();
                    for (var index = 0; index < list.Count(); ++index)
                        dictionary.Remove(list[index].Key);
                }
                if (!_factories.ContainsKey(key1) || !_factories[key1].ContainsKey(key))
                    return;
                _factories[key1].Remove(key);
            }
            finally
            {
                if (lockTaken)
                    Monitor.Exit(obj);
            }
        }

        public object GetService(Type serviceType)
        {
            return DoGetService(serviceType, _defaultKey, true);
        }

        public IEnumerable<object> GetAllInstances(Type serviceType)
        {
            var lockTaken = false;
            Dictionary<Type, Dictionary<string, Delegate>> dictionary = null;
            try
            {
                Monitor.Enter(dictionary = _factories, ref lockTaken);
                if (_factories.ContainsKey(serviceType))
                {
                    foreach (var keyValuePair in _factories[serviceType])
                        GetInstance(serviceType, keyValuePair.Key);
                }
            }
            finally
            {
                if (lockTaken)
                    Monitor.Exit(dictionary);
            }
            if (_instancesRegistry.ContainsKey(serviceType))
                return _instancesRegistry[serviceType].Values;
            return new List<object>();
        }

        public IEnumerable<TService> GetAllInstances<TService>()
        {
            return GetAllInstances(typeof (TService)).Select(instance => (TService) instance);
        }

        public object GetInstance(Type serviceType)
        {
            return DoGetService(serviceType, _defaultKey, true);
        }

        public object GetInstance(Type serviceType, string key)
        {
            return DoGetService(serviceType, key, true);
        }

        public TService GetInstance<TService>()
        {
            return (TService) DoGetService(typeof (TService), _defaultKey, true);
        }

        public TService GetInstance<TService>(string key)
        {
            return (TService) DoGetService(typeof (TService), key, true);
        }

        private static void RegisterService()
        {
            var assembly = typeof (ViewModelLocator).GetTypeInfo().Assembly;
            var listclass = assembly.CreatableTypes();
            var listservice = listclass.EndingWith("WS").AsInterfaces();
            foreach (var service in listservice)
            {
                Default.Register(service.ServiceTypes.ElementAt(0), service.ImplementationType);
            }
            var listValidator = listclass.EndingWith("Validator");
            foreach (var validator in listValidator)
            {
                if (!validator.Name.StartsWith("Abstract"))
                    Default.Register(validator);
            }
            var listviewmodel = listclass.EndingWith("ViewModel");
            foreach (var viewmodel in listviewmodel)
            {
                Default.Register(viewmodel);
            }
        }

        public void Register(Type index, Type type)
        {
            var lockTaken = false;
            object obj = null;
            try
            {
                Monitor.Enter(obj = _syncLock, ref lockTaken);
                if (_interfaceToClassMap.ContainsKey(index))
                {
                    if (_interfaceToClassMap[index] != type)
                        throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture,
                            "There is already a class registered for {0}.", (object) index.FullName));
                }
                else
                {
                    _interfaceToClassMap.Add(index, type);
                    _constructorInfos.Add(type, GetConstructorInfo(type));
                }
                Func<object> x = () => { return MakeInstance(index); };
                DoRegister(index, x, _defaultKey);
            }
            finally
            {
                if (lockTaken)
                    Monitor.Exit(obj);
            }
        }

        public void Register(Type index)
        {
            if (index.GetTypeInfo().IsInterface)
                throw new ArgumentException("An interface cannot be registered alone.");
            var lockTaken = false;
            object obj = null;
            try
            {
                Monitor.Enter(obj = _syncLock, ref lockTaken);
                if (_factories.ContainsKey(index) && _factories[index].ContainsKey(_defaultKey))
                {
                    if (!_constructorInfos.ContainsKey(index))
                        throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture,
                            "Class {0} is already registered.", (object) index));
                }
                else
                {
                    if (!_interfaceToClassMap.ContainsKey(index))
                        _interfaceToClassMap.Add(index, null);
                    _constructorInfos.Add(index, GetConstructorInfo(index));
                    Func<object> x = () => { return MakeInstance(index); };
                    DoRegister(index, x, _defaultKey);
                }
            }
            finally
            {
                if (lockTaken)
                    Monitor.Exit(obj);
            }
        }

        private object DoGetService(Type serviceType, string key, bool cache = true)
        {
            run:
            var lockTaken = false;
            object obj1 = null;
            try
            {
                Monitor.Enter(obj1 = _syncLock, ref lockTaken);
                if (string.IsNullOrEmpty(key))
                    key = _defaultKey;
                Dictionary<string, object> dictionary = null;
                if (!_instancesRegistry.ContainsKey(serviceType))
                {
                    if (!_interfaceToClassMap.ContainsKey(serviceType))
                    {
                        Register(serviceType);
                        goto run;
                        throw new Exception("chua dang ky may");
                    }
                    if (cache)
                    {
                        dictionary = new Dictionary<string, object>();
                        _instancesRegistry.Add(serviceType, dictionary);
                    }
                }
                else
                    dictionary = _instancesRegistry[serviceType];
                if (dictionary != null && dictionary.ContainsKey(key))
                    return dictionary[key];
                object obj2 = null;
                if (_factories.ContainsKey(serviceType))
                {
                    if (_factories[serviceType].ContainsKey(key))
                        obj2 = _factories[serviceType][key].DynamicInvoke(null);
                    else if (_factories[serviceType].ContainsKey(_defaultKey))
                        obj2 = _factories[serviceType][_defaultKey].DynamicInvoke(null);
                    else
                        throw new ActivationException(string.Format(CultureInfo.InvariantCulture,
                            "Type not found in cache without a key: {0}", (object) serviceType.FullName));
                }
                if (cache && dictionary != null)
                    dictionary.Add(key, obj2);
                return obj2;
            }
            finally
            {
                if (lockTaken)
                    Monitor.Exit(obj1);
            }
        }

        private void DoRegister<TClass>(Type classType, Func<TClass> factory, string key)
        {
            if (_factories.ContainsKey(classType))
            {
                if (_factories[classType].ContainsKey(key))
                    return;
                _factories[classType].Add(key, factory);
            }
            else
            {
                var dictionary = new Dictionary<string, Delegate>
                {
                    {
                        key,
                        factory
                    }
                };
                _factories.Add(classType, dictionary);
            }
        }

        private void DoRegister(Type classType, Func<object> factory, string key)
        {
            if (_factories.ContainsKey(classType))
            {
                if (_factories[classType].ContainsKey(key))
                    return;
                _factories[classType].Add(key, factory);
            }
            else
            {
                var dictionary = new Dictionary<string, Delegate>
                {
                    {
                        key,
                        factory
                    }
                };
                _factories.Add(classType, dictionary);
            }
        }

        private ConstructorInfo GetConstructorInfo(Type serviceType)
        {
            var type = !_interfaceToClassMap.ContainsKey(serviceType)
                ? serviceType
                : _interfaceToClassMap[serviceType] ?? serviceType;
            var constructorInfoArray = type.GetTypeInfo().DeclaredConstructors.Where(c => c.IsPublic).ToArray();
            if (constructorInfoArray.Length > 1)
            {
                if (constructorInfoArray.Length > 2)
                    return GetPreferredConstructorInfo(constructorInfoArray, type);
                if (constructorInfoArray.FirstOrDefault(i => i.Name == ".cctor") == null)
                    return GetPreferredConstructorInfo(constructorInfoArray, type);
                var constructorInfo = constructorInfoArray.FirstOrDefault(i => i.Name != ".cctor");
                if (constructorInfo == null || !constructorInfo.IsPublic)
                    throw new ActivationException(string.Format(CultureInfo.InvariantCulture,
                        "Cannot register: No public constructor found in {0}.", (object) type.Name));
                return constructorInfo;
            }
            if (constructorInfoArray.Length == 0 ||
                constructorInfoArray.Length == 1 && !constructorInfoArray[0].IsPublic)
                throw new ActivationException(string.Format(CultureInfo.InvariantCulture,
                    "Cannot register: No public constructor found in {0}.", (object) type.Name));
            return constructorInfoArray[0];
        }

        private static ConstructorInfo GetPreferredConstructorInfo(IEnumerable<ConstructorInfo> constructorInfos,
            Type resolveTo)
        {
            var constructorInfo = constructorInfos.Select(t => new
            {
                t,
                attribute = t.GetCustomAttribute(typeof (PreferredConstructorAttribute))
            }).Where(param0 => param0.attribute != null).Select(param0 => param0.t).FirstOrDefault();
            if (constructorInfo == null)
                throw new ActivationException(string.Format(CultureInfo.InvariantCulture,
                    "Cannot register: Multiple constructors found in {0} but none marked with PreferredConstructor.",
                    (object) resolveTo.Name));
            return constructorInfo;
        }

        private TClass MakeInstance<TClass>() where TClass : class
        {
            var index = typeof (TClass);
            var constructorInfo = _constructorInfos.ContainsKey(index)
                ? _constructorInfos[index]
                : GetConstructorInfo(index);
            var parameters1 = constructorInfo.GetParameters();
            if (parameters1.Length == 0)
                return (TClass) constructorInfo.Invoke(_emptyArguments);
            var parameters2 = new object[parameters1.Length];
            foreach (var parameterInfo in parameters1)
                parameters2[parameterInfo.Position] = GetService(parameterInfo.ParameterType);
            return (TClass) constructorInfo.Invoke(parameters2);
        }

        private object MakeInstance(Type index)
        {
            var constructorInfo = _constructorInfos.ContainsKey(index)
                ? _constructorInfos[index]
                : GetConstructorInfo(index);
            var parameters1 = constructorInfo.GetParameters();
            if (parameters1.Length == 0)
                return constructorInfo.Invoke(_emptyArguments);
            var parameters2 = new object[parameters1.Length];
            foreach (var parameterInfo in parameters1)
                parameters2[parameterInfo.Position] = GetService(parameterInfo.ParameterType);
            return constructorInfo.Invoke(parameters2);
        }

        public IEnumerable<object> GetAllCreatedInstances(Type serviceType)
        {
            if (_instancesRegistry.ContainsKey(serviceType))
                return _instancesRegistry[serviceType].Values;
            return new List<object>();
        }

        public object GetInstanceWithoutCaching(Type serviceType)
        {
            return DoGetService(serviceType, _defaultKey, false);
        }

        public object GetInstanceWithoutCaching(Type serviceType, string key)
        {
            return DoGetService(serviceType, key, false);
        }

        public TService GetInstanceWithoutCaching<TService>()
        {
            return (TService) DoGetService(typeof (TService), _defaultKey, false);
        }

        public TService GetInstanceWithoutCaching<TService>(string key)
        {
            return (TService) DoGetService(typeof (TService), key, false);
        }
    }

    public static class TypeExtent
    {
        public static IEnumerable<Type> CreatableTypes(this Assembly assembly)
        {
            return ExceptionSafeGetTypes(assembly)
                .Select(t => t.GetTypeInfo())
                .Where(t => !t.IsAbstract)
                .Where(t => t.DeclaredConstructors.Any(c =>
                {
                    if (!c.IsStatic)
                        return c.IsPublic;
                    return false;
                })).Select(t => t.AsType());
        }

        public static IEnumerable<Type> ExceptionSafeGetTypes(this Assembly assembly)
        {
            try
            {
                return GetTypes(assembly);
            }
            catch (ReflectionTypeLoadException ex)
            {
                return new Type[0];
            }
        }

        public static IEnumerable<Type> GetTypes(this Assembly assembly)
        {
            return assembly.DefinedTypes.Select(t => t.AsType());
        }

        public static IEnumerable<Type> EndingWith(this IEnumerable<Type> types, string endingWith)
        {
            return types.Where(x => x.Name.EndsWith(endingWith));
        }

        public static IEnumerable<ServiceTypeAndImplementationTypePair> AsInterfaces(this IEnumerable<Type> types)
        {
            return types.Select(t => new ServiceTypeAndImplementationTypePair(GetInterfaces(t).ToList(), t));
        }

        public static IEnumerable<Type> GetInterfaces(this Type type)
        {
            return type.GetTypeInfo().ImplementedInterfaces;
        }

        public class ServiceTypeAndImplementationTypePair
        {
            public ServiceTypeAndImplementationTypePair(List<Type> serviceTypes, Type implementationType)
            {
                ImplementationType = implementationType;
                ServiceTypes = serviceTypes;
            }

            public List<Type> ServiceTypes { get; }
            public Type ImplementationType { get; }
        }
    }

    #endregion
}