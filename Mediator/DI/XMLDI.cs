using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Mediator
{
    public class XMLDI: DI
    {
        private Dictionary<string, object> instanceMap = new Dictionary<string, object>();
        private Dictionary<string, DIInstanceDescription> factoryMap = new Dictionary<string, DIInstanceDescription>();
        private Dictionary<string, Builder> builderMap = new Dictionary<string, Builder>();

        public XMLDI(string url)
        {
            Dictionary<string, Action<XElement>> actionMap = new Dictionary<string, Action<XElement>>
            {
                {"Object", (element)=>{ addObjectFromElement(element); } },
                {"Factory", (element)=>{ addFactoryFromElement(element); } },
                {"Builder", (element)=>{ addBuilderFromElement(element); } }
            };
            XElement xml = XElement.Load(url);
            foreach(XElement element in xml.Elements())
            {
                actionMap[element.Name.LocalName](element);
            }
        }

        protected override object GetInstance(string name)
        {
            if(instanceMap.ContainsKey(name))
            {
                return instanceMap[name];
            }

            if (builderMap.ContainsKey(name))
            {
                Builder builder = builderMap[name];
                object inatance = builder.Create();
                instanceMap[name] = inatance;
                return inatance;
            }
            return null;
        }

        protected override object CreateInstance(string name)
        {
            if(factoryMap.ContainsKey(name))
            {
                DIInstanceDescription description = factoryMap[name];
                object instance = createObjectFromDescription(description);
                return instance;
            }
            if (builderMap.ContainsKey(name))
            {
                Builder builder = builderMap[name];
                object inatance = builder.Create();
                return inatance;
            }
            return null;
        }

        private void addObjectFromElement(XElement element)
        {
            DIInstanceDescription description = createDescriptionFromElement(element);
            object instance = createObjectFromDescription(description);
            instanceMap[description.InstanceName] = instance;
        }

        private void addFactoryFromElement(XElement element)
        {
            DIInstanceDescription description = createDescriptionFromElement(element);
            factoryMap[description.InstanceName] = description;
        }

        private void addBuilderFromElement(XElement element)
        {
            DIInstanceDescription description = createDescriptionFromElement(element);
            Builder builder = (Builder)createObjectFromDescription(description);
            builderMap[description.InstanceName] = builder;
        }

        private DIInstanceDescription createDescriptionFromElement(XElement element)
        {
            DIInstanceDescription description = new DIInstanceDescription();
            putAttributes(element, description);
            putContent(element, description);
            return description;
        }

        private void putAttributes(XElement element, DIInstanceDescription description)
        {
            foreach (XAttribute attribute in element.Attributes())
            {
                if (attribute.Name.LocalName.Equals("type"))
                {
                    description.TypeName = attribute.Value;
                }
                else if (attribute.Name.LocalName.Equals("assembly"))
                {
                    description.AssemblyName = attribute.Value;
                }
                else if (attribute.Name.LocalName.Equals("name"))
                {
                    description.InstanceName = attribute.Value;
                }
            }
        }

        private void putContent(XElement element, DIInstanceDescription description)
        {
            Dictionary<string, object> content = parseElement(element);
            description.content = content;
        }      
        
        private Dictionary<string, object> parseElement(XElement element)
        {
            Dictionary<string, object> content = new Dictionary<string, object>();
            foreach (XElement contentElement in element.Elements())
            {
                string name = contentElement.Name.LocalName;
                object value = null;
                if (name.Equals("List"))
                {
                    KeyValuePair<string, object> pair = parseList(contentElement);
                    name = pair.Key;
                    value = pair.Value;
                } else
                {
                    value = parseContentElementItem(contentElement);
                }

                content[name] = value;
            }
            return content;
        }

        private object parseContentElementItem(XElement element)
        {
            if (element.HasElements)
            {
                return parseElement(element);
            }
            return element.Value;
        }

        private KeyValuePair<string, object> parseList(XElement element)
        {
            string name = getNameFromElementAttributes(element);
            List<object> list = getListFromElementContent(element);
            KeyValuePair<string, object> result = new KeyValuePair<string, object>(name, list);
            return result;
        }

        private string getNameFromElementAttributes(XElement element)
        {
            foreach(XAttribute attribute in element.Attributes())
            {
                if(attribute.Name.LocalName.Equals("name"))
                {
                    return attribute.Value;
                }
            }
            return null;
        }

        private List<object> getListFromElementContent(XElement element)
        {
            List<object> list = new List<object>();
            foreach(XElement elementItem in element.Elements())
            {
                object item = parseContentElementItem(elementItem);
                list.Add(item);
            }
            return list;
        }


        private object createObjectFromDescription(DIInstanceDescription description)
        {
            if (description.TypeName != null)
            {
                if (description.content.Count == 0) { 
                    return Activator.CreateInstance(
                    description.AssemblyName,
                    description.TypeName).Unwrap();
                }
                return Activator.CreateInstance(
                description.AssemblyName,
                description.TypeName,
                false, 0, null,     
                new object[] { description.content },
                null, null).Unwrap();
            }
            Console.WriteLine("Could not create the instance");
            return null;
        }

        protected override void SetInstance(string name, object instance)
        {
            instanceMap[name] = instance;
        }
    }
}
