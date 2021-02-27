﻿using System;
using System.Linq;
using System.Reflection;
using EntityComponentSystemClassLibrary.ECS;

namespace EntityComponentSystemClassLibrary
{
    public class Program
    {
        private static void Main()
        {
            var componentFactory = new ComponentFactory(Assembly.GetExecutingAssembly());

            Entity TestEntity = new Entity(componentFactory);
            TestEntity.AddComponents(new ComponentParameterList<IComponent>()
            {
                new ComponentParameters<TestComponent2>(),
                new ComponentParameters<TestComponent3>("Fuck me sideways and call me a jiggly-puff, this worked batman")
            });

            var response = TestEntity.GetComponent<TestComponent2>().MyStatus();
            Console.WriteLine(response);

            response = TestEntity.GetComponent<TestComponent3>().GetResponse();
            Console.WriteLine(response);

            TestEntity.RemoveComponent<TestComponent3>();
            TestEntity.AddComponent<TestComponent3>("This is a new Component 3");

            response = TestEntity.GetComponent<TestComponent3>().GetResponse();
            Console.WriteLine(response);


            var components = TestEntity.GetComponentNames();
            foreach (string name in components)
            {
                char character = name != components.Last() ? ',' : ' ';
                Console.Write($"[{name}]{character}");
            }
        }
    }

    [Exclude]
    public class TestComponent : IComponent
    {
        public string Exists()
        {
            return "I do in fact exist";
        }
    }

    public class TestComponent2 : IComponent
    {
        public string MyStatus()
        {
            return "Yeah I am doing alright";
        }
    }

    public class TestComponent3 : IComponent
    {
        private string Response;

        public TestComponent3(string _response)
        {
            Response = _response;
        }

        public string GetResponse()
        {
            return Response;
        }
    }
}