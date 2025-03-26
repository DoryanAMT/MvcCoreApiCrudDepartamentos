using MvcCoreApiCrudDepartamentos.Models;
using Newtonsoft.Json;
using NuGet.Configuration;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;

namespace MvcCoreApiCrudDepartamentos.Services
{
    public class ServiceDepartamentos
    {
        private string UrlApi;
        private MediaTypeWithQualityHeaderValue header;
        public ServiceDepartamentos(IConfiguration configuration)
        {
            this.header = new MediaTypeWithQualityHeaderValue("application/json");
            this.UrlApi = configuration.GetValue<string>("ApiUrls:ApiDepartamentos");
        }
        public async Task<List<Departamento>> GetDepartamentosAsync()
        {
            string request = "api/departamentos";
            List<Departamento> data = await this.CallApiAsync<List<Departamento>>(request);
            return data;
        }

        public async Task<Departamento> FindDepartamentoAsync
            (int idDepartamento)
        {
            string request = "api/departamentos/"+idDepartamento;
            Departamento data = await this.CallApiAsync<Departamento>(request);
            return data;
        }
        //public async Task InsertDepartamento()
        //{

        //}


        private async Task<T> CallApiAsync<T>
            (string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                HttpResponseMessage response =
                    await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await
                        response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }
        //  LOS METODOS DE ACCION QUE RECIBEN OBJETOS PUEDEN SER
        //  GENERICOS Y RECIBIR T
        //  SI LOS METODOS RECIBEN LA INFORMACION POR URL
        //  SI QUE NO SUELEN SER GENERICOS
        public async Task InsertDepartamentoAsync
            (int id, string nombre, string localidad)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/departamentos";
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                //  DEBEMOS CREAR NUESTRO MODEL A ENVIAR
                Departamento dept = new Departamento();
                dept.IdDepartamento = id;
                dept.Nombre = nombre;
                dept.Localidad = localidad;
                //  CONVERTIMOS NUESTRO MODEL A JSON
                string json = JsonConvert.SerializeObject(dept);
                //  PARA ENVIAR DATOS SE UTILIZA StringContent
                //  DONDE DEBEMOS ENVIAR EL JSON, EL FORMATO Y EL TIPO
                StringContent content = new StringContent
                    (json, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                    await client.PostAsync(request, content);
            }
        }
        public async Task UpdateDepartamentoAsync
            (int id, string nombre, string localidad)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/departamentos";
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                Departamento departamento = new Departamento();
                departamento.IdDepartamento = id;
                departamento.Nombre = nombre;
                departamento.Localidad = localidad;
                string json = JsonConvert.SerializeObject(departamento);
                StringContent content = new StringContent
                    (json, Encoding.UTF8, "application/json");
                await client.PutAsync(request, content);
            }
        }
        public async Task DeleteDepartamento
            (int idDepartamento)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "api/departamentos/"+idDepartamento;
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);
                HttpResponseMessage response =
                    await client.DeleteAsync(request);
            }
        }
    }
}
