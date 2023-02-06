using System;
using System.Linq;
using System.Threading.Tasks;
using LdapForNet;
using LdapForNet.Native;

using static LdapForNet.Native.Native;

namespace AbpLdapSample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //await ABPSample(new LdapParameters());

            //Console.WriteLine();

            await GitHubExample(new LdapParameters());

            Console.ReadKey();
        }

        static async Task ABPSample(LdapParameters ldap)
        {
            Console.WriteLine(new string('-', 16));
            Console.WriteLine("From ABP Sample");
            Console.WriteLine(new string('-', 16));

            using (var ldapConnection = new LdapConnection())
            {
                ldapConnection.Connect(ldap.server, ldap.port, Native.LdapSchema.LDAP, Native.LdapVersion.LDAP_VERSION3);
                try
                {
                    await ldapConnection.BindAsync(Native.LdapAuthType.ExternalAd, new LdapCredential
                    {
                        // Configure username according to your LDAP config:
                        // cn=admin,dc=abp,dc=com or just username.
                        UserName = $"cn={ldap.adminUsername},{ldap.baseDc}",
                        Password = ldap.adminPassword
                    });

                    Console.WriteLine($"{ldap.adminUsername} login success!");
                    Console.WriteLine();

                    await ldapConnection.BindAsync(Native.LdapAuthType.ExternalAd, new LdapCredential
                    {
                        // Configure username according to your LDAP config:
                        // cn=admin,dc=abp,dc=com or just username.
                        UserName = $"uid=utility,{ldap.baseDc}",
                        Password = "97f0f96a7a4793e1df6fa9f46246e18e"
                    });

                    Console.WriteLine($"utility login success!");
                    Console.WriteLine();

                    await ldapConnection.BindAsync(Native.LdapAuthType.ExternalAd, new LdapCredential
                    {
                        UserName = $"cn=amnil,{ldap.baseDc}",
                        Password = ldap.testPassword
                    });

                    Console.WriteLine($"amnil login success!");
                    Console.WriteLine();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Exception = {e.Message}");
                }

            }
        }

        static async Task GitHubExample(LdapParameters ldap)
        {
            Console.WriteLine(new string('-', 32));
            Console.WriteLine("From LdapForNet GitHub Example");
            Console.WriteLine(new string('-', 32));

            try
            {
                using (var cn = new LdapConnection())
                {
                    //cn.Connect(new Uri($"ldaps://portainer.server247.info:{ldap.port}"));
                    cn.Connect(ldap.server, ldap.port, LdapSchema.LDAP, LdapVersion.LDAP_VERSION3);
                    Console.WriteLine($"Connection with [portainer.server247.info] Success!");
                    Console.WriteLine();

                    await cn.BindAsync(Native.LdapAuthType.ExternalAd, new LdapCredential
                    {
                        UserName = $"cn={ldap.adminUsername},{ldap.baseDc}",
                        Password = ldap.adminPassword
                    });
                    Console.WriteLine($"{ldap.adminUsername} login success!");
                    Console.WriteLine();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }

    public class LdapParameters
    {
        /// <summary>
        /// IP Address for portainer.server247.info
        /// </summary>
        public string server { get; } = "macbank.com";
        public int port { get; } = 389;
        public string baseDc { get; } = "OU=Operations 1,DC=macbank,DC=com";
        public string adminUsername { get; } = "dmstest01";
        public string adminPassword { get; } = "nepal@123";
        public string testUsername { get; } = "dmstest02";
        public string testPassword { get; } = "nepal@123";
    }
}