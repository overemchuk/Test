using SharpRepository.XmlRepository;

namespace TestApp.Data
{
    public class CustomXmlRepository<T> : XmlRepository<T> where T : class, new()
    {
        public CustomXmlRepository(string storagePath)
            : base(storagePath)
        {
        }

        public void Save()
        {
            base.SaveChanges();
        }
    }
}