using System.Net.Http.Formatting;
using FluentAssertions;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Personal.Soft.TEST
{


    public class FlowControllerTest
    {
        static string token = "";

        static HttpClient TestClient = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
        {
            var id = Guid.NewGuid().ToString();
            string dbTesting = $"Persona_Soft_{id}";
            builder.UseSetting("ServiceSettings:ServiceName", dbTesting);

    }).CreateClient();
        public class PlanResponse
        {
            public Guid id { get; set; }
            public string nombrePlan { get; set; }

        }

        public class ClienteResponse
        {
            public Guid id { get; set; }
            public string identificacion { get; set; } = string.Empty;
            public string nombreCliente { get; set; } = string.Empty;
            public string ciudad { get; set; } = string.Empty;
            public string direccion { get; set; } = string.Empty;
            public DateTime fechaNacimiento { get; set; }

        }

        public class RegistroResponse
        {
            public string message { get; set; }
            public string token { get; set; }

        }

        public FlowControllerTest()
        {

        }



        [Fact]
        public async void A_RegisterUserSuccess()
        {
   
      
            var request = new
            {
                names = "admin",
                email = "admin@gmail.com",
                password = "123456789"
                
            };

            var carga = TestClient.PostAsync($"/api/auth/register", request, new JsonMediaTypeFormatter()).Result;
            carga.EnsureSuccessStatusCode();

            var interactionResponse = JsonSerializer.Deserialize<Dictionary<string, string>>(carga.Content.ReadAsStringAsync().Result);
            var token = interactionResponse["token"];
            var message = interactionResponse["message"];
            FlowControllerTest.token = token;
            TestClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            message.Should().Be("Registro Correcto");

        }

        
        [Fact]
        public async void B_CreatePlanSuccess()
        {
            var planRequest = new
            {
                nombrePlan = "Plan Unico"
            };
                                                
            var carga = TestClient.PostAsync($"/api/plan", planRequest, new JsonMediaTypeFormatter()).Result;
            carga.EnsureSuccessStatusCode();

            var interactionResponse = JsonSerializer.Deserialize<Dictionary<string, string>>(carga.Content.ReadAsStringAsync().Result);
            var idPlan = interactionResponse["id"];
            var message = interactionResponse["message"];
            message.Should().Be("Plan creado Correctamente");

            var c = TestClient.GetAsync($"/api/plan/{idPlan}").Result;
            c.EnsureSuccessStatusCode();
            var response = c.Content.ReadAsStringAsync().Result;
            var respuestaConsulta = JsonSerializer.Deserialize<PlanResponse>(response);
            respuestaConsulta.id.Should().Be(idPlan);
            respuestaConsulta.nombrePlan.Should().Be(planRequest.nombrePlan);
        }


        [Fact]
        public async void Z_CreateClienteSuccess()
        {            
            var request = new
            {
                nombreCliente = "PEPE PEREZ",
                identificacion = "JHX456789",
                ciudad = "BOGOTA",
                direccion = "CLL 140 DG 4",
                fechaNacimiento = DateTime.Now
            };

            var carga = TestClient.PostAsync($"/api/cliente", request, new JsonMediaTypeFormatter()).Result;
            carga.EnsureSuccessStatusCode();
            var interactionResponse = JsonSerializer.Deserialize<Dictionary<string, string>>(carga.Content.ReadAsStringAsync().Result);
            var idCliente = interactionResponse["id"];
            var message = interactionResponse["message"];
            message.Should().Be("Cliente creado Correctamente");

            var c = TestClient.GetAsync($"/api/cliente/{idCliente}").Result;
            c.EnsureSuccessStatusCode();
            var response = c.Content.ReadAsStringAsync().Result;
            var respuestaConsulta = JsonSerializer.Deserialize<ClienteResponse>(response);
            respuestaConsulta.id.Should().Be(idCliente);
            respuestaConsulta.nombreCliente.Should().Be(request.nombreCliente);



        }



    }
}