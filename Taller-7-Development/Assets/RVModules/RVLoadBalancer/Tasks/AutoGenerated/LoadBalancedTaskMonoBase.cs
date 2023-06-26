// Created by Ronis Vision. All rights reserved
// 02.08.2020 20:56:21
// ======AUTOGENERATED - DONT EDIT======

using UnityEngine;
using System;
using Object = UnityEngine.Object;
using RVModules.RVLoadBalancer.Tasks;

public abstract class LoadBalancedTaskMonoBase : MonoBehaviour
{
#region Fields
// DummyMonoBehaviour
// LoadBalancedTaskWrapper
        private ILoadBalancedTask task;
        private Action onTaskStart;
        private Action onTaskFinish;
        protected abstract void TaskUpdate(float _dt);
        protected abstract void OnTaskStart();
        protected abstract void OnTaskFinish();
#endregion
#region Properties
// DummyMonoBehaviour
// LoadBalancedTaskWrapper
        public ILoadBalancedTask Task => task;
#endregion
#region Methods
// DummyMonoBehaviour
// LoadBalancedTaskWrapper
public void          LoadBalancedTaskWrapper(int _priority = 0, string _name = "")
        {
            task = new LoadBalancedTask(TaskUpdate, OnTaskStart, OnTaskFinish, _priority, _name);
        }
        public static ILoadBalancedTask ToILoadBalancedTask(LoadBalancedTaskWrapper task) => task.Task;
#endregion
}