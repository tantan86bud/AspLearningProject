/* 
 * API
 *
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
 *
 * The version of the OpenAPI document: v1
 * 
 * Generated by: https://github.com/openapitools/openapi-generator.git
 */

using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using RestSharp;
using NUnit.Framework;

using Org.OpenAPITools.Client;
using Org.OpenAPITools.Api;
using Org.OpenAPITools.Model;

namespace Org.OpenAPITools.Test
{
    /// <summary>
    ///  Class for testing ProductApi
    /// </summary>
    /// <remarks>
    /// This file is automatically generated by OpenAPI Generator (https://openapi-generator.tech).
    /// Please update the test case below to test the API endpoint.
    /// </remarks>
    public class ProductApiTests
    {
        private ProductApi instance;

        /// <summary>
        /// Setup before each unit test
        /// </summary>
        [SetUp]
        public void Init()
        {
            instance = new ProductApi();
        }

        /// <summary>
        /// Clean up after each unit test
        /// </summary>
        [TearDown]
        public void Cleanup()
        {

        }

        /// <summary>
        /// Test an instance of ProductApi
        /// </summary>
        [Test]
        public void InstanceTest()
        {
            // TODO uncomment below to test 'IsInstanceOf' ProductApi
            //Assert.IsInstanceOf(typeof(ProductApi), instance);
        }

        
        /// <summary>
        /// Test ApiProductGet
        /// </summary>
        [Test]
        public void ApiProductGetTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //var response = instance.ApiProductGet();
            //Assert.IsInstanceOf(typeof(List<ProductResource>), response, "response is List<ProductResource>");
        }
        
        /// <summary>
        /// Test ApiProductIdDelete
        /// </summary>
        [Test]
        public void ApiProductIdDeleteTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //int id = null;
            //instance.ApiProductIdDelete(id);
            
        }
        
        /// <summary>
        /// Test ApiProductIdPut
        /// </summary>
        [Test]
        public void ApiProductIdPutTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //int id = null;
            //ProductEditResource productEditResource = null;
            //instance.ApiProductIdPut(id, productEditResource);
            
        }
        
        /// <summary>
        /// Test ApiProductPost
        /// </summary>
        [Test]
        public void ApiProductPostTest()
        {
            // TODO uncomment below to test the method and replace null with proper value
            //ProductEditResource productEditResource = null;
            //instance.ApiProductPost(productEditResource);
            
        }
        
    }

}
