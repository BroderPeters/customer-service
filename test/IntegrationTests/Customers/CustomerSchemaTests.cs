using System;
using System.Threading.Tasks;
using CodeChallenge.CustomerService.Customers;
using CodeChallenge.CustomerService.Infrastructure.Contexts;
using CodeChallenge.CustomerService.Repositories;
using CodeChallenge.CustomerService.Services;
using HotChocolate;
using HotChocolate.Execution;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Snapshooter.Xunit;
using Xunit;

namespace CodeChallenge.IntegrationTests.Customers
{
    public class CustomerSchemaTests
    {
        private Task _initialziation;
        private ISchema _schema;
        private IRequestExecutor _requestExecutor;

        public CustomerSchemaTests()
        {
            _initialziation = InitializeAsync();
        }

        [Fact]
        public void Customer_SchemaValidation()
        {
            _schema.Print().MatchSnapshot();
        }

        [Fact]
        public async Task GetCustomers_SchemaValidation()
        {
            await _requestExecutor.ExecuteAsync(@"
                mutation AddCustomer {
                    addCustomer(input: {
                        name: ""John Doe"",
                        email: ""john.doe@mail.com"",
                        isBlocked: false,
                        status: ACTIVE
                    }) {
                        customer {
                            id
                        }
                    }
                }");

            var result = await _requestExecutor.ExecuteAsync(@"
                {
                    customers {
                        nodes {
                            id,
                            name,
                            email,
                            isBlocked,
                            status,
                            code
                        }
                    }
                }");

            result.ToJson().MatchSnapshot();

            // TODO: Find a way to ignore a field inside a JArray. Context: Property 'createdAt' not valid on JArray.
            // 'createdAt' not used for now to make the test repeatable
            // result.ToJson().MatchSnapshot(options => options.IgnoreField("data.customers.nodes.createdAt"));
        }

        [Fact]
        public async Task GetCustomer_SchemaValidation()
        {
            await _requestExecutor.ExecuteAsync(@"
                mutation AddCustomer {
                    addCustomer(input: {
                        name: ""John Doe"",
                        email: ""john.doe@mail.com"",
                        isBlocked: false,
                        status: ACTIVE
                    }) {
                        customer {
                            id
                        }
                    }
                }");

            var result = await _requestExecutor.ExecuteAsync(@"
               { 
                    customer(id: 1) {
                        name,
                        email,
                        isBlocked,
                        status,
                        code,
                        createdAt
                    }
                }");

            result.ToJson().MatchSnapshot(options => options.IgnoreField("data.customer.createdAt"));
        }

        [Fact]
        public async Task AddCustomer_SchemaValidation()
        {
            var result = await _requestExecutor.ExecuteAsync(@"
                mutation AddCustomer {
                    addCustomer(input: {
                        name: ""John Doe"",
                        email: ""john.doe@mail.com"",
                        isBlocked: false,
                        status: ACTIVE
                    }) {
                        customer {
                            id,
                            name,
                            email,
                            isBlocked,
                            status,
                            code,
                            createdAt
                        },
                        errors {
                            code,
                            message
                        }
                    }
                }");

            result.ToJson().MatchSnapshot(options => options.IgnoreField("data.addCustomer.customer.createdAt"));
        }

        [Fact]
        public async Task AddCustomer_EmailMaxStringLengthExceeded_ReturnsMaxStringLengthExceededError()
        {
            var result = await _requestExecutor.ExecuteAsync(@"
                mutation AddCustomer {
                    addCustomer(input: {
                        name: ""John Doe"",
                        email: ""prettylongemailaddressprettylongemailaddressprettylongemailaddressprettylongemailaddressprettylongemailaddress@prettylongemailaddress.com"",
                        isBlocked: false,
                        status: ACTIVE
                    }) {
                        customer {
                            id,
                            name,
                            email,
                            isBlocked,
                            status,
                            code,
                            createdAt
                        },
                        errors {
                            code,
                            message
                        }
                    }
                }");

            result.ToJson().MatchSnapshot();
        }

        [Fact]
        public async Task AddCustomer_NameMaxStringLengthExceeded_ReturnsMaxStringLengthExceededError()
        {
            var result = await _requestExecutor.ExecuteAsync(@"
                mutation AddCustomer {
                    addCustomer(input: {
                        name: ""prettylongnameprettylongnameprettylongnameprettylongnameprettylongnameprettylongnameprettylongnameprettylongnameprettylongnameprettylongname"",
                        email: ""john.doe@mail.com"",
                        isBlocked: false,
                        status: ACTIVE
                    }) {
                        customer {
                            id,
                            name,
                            email,
                            isBlocked,
                            status,
                            code,
                            createdAt
                        },
                        errors {
                            code,
                            message
                        }
                    }
                }");

            result.ToJson().MatchSnapshot();
        }

        [Fact]
        public async Task AddCustomer_InvalidEmail_ReturnsError()
        {
            var result = await _requestExecutor.ExecuteAsync(@"
                mutation AddCustomer {
                    addCustomer(input: {
                        name: ""John Doe"",
                        email: ""InvalidEmailAddress"",
                        isBlocked: false,
                        status: ACTIVE
                    }) {
                        customer {
                            id,
                            name,
                            email,
                            isBlocked,
                            status,
                            code,
                            createdAt
                        },
                        errors {
                            code,
                            message
                        }
                    }
                }");

            result.ToJson().MatchSnapshot();
        }

        [Fact]
        public async Task UpdateCustomer_SchemaValidation()
        {
            await _requestExecutor.ExecuteAsync(@"
                mutation AddCustomer {
                    addCustomer(input: {
                        name: ""John Doe"",
                        email: ""john.doe@mail.com"",
                        isBlocked: false,
                        status: ACTIVE
                    }) {
                        customer {
                            id
                        }
                    }
                }");

            var result = await _requestExecutor.ExecuteAsync(@"
                mutation UpdateCustomer {
                    updateCustomer(input: {
                        id: 1
                        name: ""John Doe"",
                        email: ""john.doe@mail.com"",
                        isBlocked: false,
                        status: ACTIVE
                    }) {
                        customer {
                            id,
                            name,
                            email,
                            isBlocked,
                            status,
                            code,
                            createdAt
                        },
                        errors {
                            code,
                            message
                        }
                    }
                }");

            result.ToJson().MatchSnapshot(options => options.IgnoreField("data.updateCustomer.customer.createdAt"));
        }

        [Fact]
        public async Task UpdateCustomer_CustomerNotFound_CustomerNotFoundException()
        {
            var result = await _requestExecutor.ExecuteAsync(@"
                mutation UpdateCustomer {
                    updateCustomer(input: {
                        id: 1
                        name: ""John Doe"",
                        email: ""john.doe@mail.com"",
                        isBlocked: false,
                        status: ACTIVE
                    }) {
                        customer {
                            id,
                            name,
                            email,
                            isBlocked,
                            status,
                            code,
                            createdAt
                        },
                        errors {
                            code,
                            message
                        }
                    }
                }");

            result.ToJson().MatchSnapshot();
        }

        [Fact]
        public async Task UpdateCustomer_NameMaxStringLengthExceeded_ReturnsMaxStringLengthExceededError()
        {
            await _requestExecutor.ExecuteAsync(@"
                mutation AddCustomer {
                    addCustomer(input: {
                        name: ""John Doe"",
                        email: ""john.doe@mail.com"",
                        isBlocked: false,
                        status: ACTIVE
                    }) {
                        customer {
                            id
                        }
                    }
                }");

            var result = await _requestExecutor.ExecuteAsync(@"
                mutation UpdateCustomer {
                    updateCustomer(input: {
                        id: 1
                        name: ""prettylongnameprettylongnameprettylongnameprettylongnameprettylongnameprettylongnameprettylongnameprettylongnameprettylongnameprettylongname"",
                        email: ""john.doe@mail.com"",
                        isBlocked: false,
                        status: ACTIVE
                    }) {
                        customer {
                            id,
                            name,
                            email,
                            isBlocked,
                            status,
                            code,
                            createdAt
                        },
                        errors {
                            code,
                            message
                        }
                    }
                }");

            result.ToJson().MatchSnapshot();
        }

        [Fact]
        public async Task UpdateCustomer_EmailMaxStringLengthExceeded_ReturnsMaxStringLengthExceededError()
        {
            await _requestExecutor.ExecuteAsync(@"
                mutation AddCustomer {
                    addCustomer(input: {
                        name: ""John Doe"",
                        email: ""john.doe@mail.com"",
                        isBlocked: false,
                        status: ACTIVE
                    }) {
                        customer {
                            id
                        }
                    }
                }");

            var result = await _requestExecutor.ExecuteAsync(@"
                mutation UpdateCustomer {
                    updateCustomer(input: {
                        id: 1
                        name: ""John Doe"",
                        email: ""prettylongemailaddressprettylongemailaddressprettylongemailaddressprettylongemailaddressprettylongemailaddress@prettylongemailaddress.com"",
                        isBlocked: false,
                        status: ACTIVE
                    }) {
                        customer {
                            id,
                            name,
                            email,
                            isBlocked,
                            status,
                            code,
                            createdAt
                        },
                        errors {
                            code,
                            message
                        }
                    }
                }");

            result.ToJson().MatchSnapshot();
        }

        [Fact]
        public async Task UpdateCustomer_InvalidEmail_ReturnsError()
        {
            await _requestExecutor.ExecuteAsync(@"
                mutation AddCustomer {
                    addCustomer(input: {
                        name: ""John Doe"",
                        email: ""john.doe@mail.com"",
                        isBlocked: false,
                        status: ACTIVE
                    }) {
                        customer {
                            id
                        }
                    }
                }");

            var result = await _requestExecutor.ExecuteAsync(@"
                mutation UpdateCustomer {
                    updateCustomer(input: {
                        id: 1
                        name: ""John Doe"",
                        email: ""InvalidEmailAddress"",
                        isBlocked: false,
                        status: ACTIVE
                    }) {
                        customer {
                            id,
                            name,
                            email,
                            isBlocked,
                            status,
                            code,
                            createdAt
                        },
                        errors {
                            code,
                            message
                        }
                    }
                }");

            result.ToJson().MatchSnapshot();
        }

        [Fact]
        public async Task DeleteCustomer_SchemaValidation()
        {
            await _requestExecutor.ExecuteAsync(@"
                mutation AddCustomer {
                    addCustomer(input: {
                        name: ""John Doe"",
                        email: ""john.doe@mail.com"",
                        isBlocked: false,
                        status: ACTIVE
                    }) {
                        customer {
                            id
                        }
                    }
                }");

            var result = await _requestExecutor.ExecuteAsync(@"
                mutation DeleteCustomer {
                    deleteCustomer(input: {
                        id: 1
                    }) {
                        customer {
                            id,
                            name,
                            email,
                            isBlocked,
                            status,
                            code,
                            createdAt
                        },
                        errors {
                            code,
                            message
                        }
                    }
                }");

            result.ToJson().MatchSnapshot(options => options.IgnoreField("data.deleteCustomer.customer.createdAt"));
        }

        [Fact]
        public async Task DeleteCustomer_CustomerNotFound_CustomerNotFoundException()
        {
            var result = await _requestExecutor.ExecuteAsync(@"
                mutation DeleteCustomer {
                    deleteCustomer(input: {
                        id: 1
                    }) {
                        customer {
                            id,
                            name,
                            email,
                            isBlocked,
                            status,
                            code,
                            createdAt
                        },
                        errors {
                            code,
                            message
                        }
                    }
                }");

            result.ToJson().MatchSnapshot();
        }

        private async Task InitializeAsync()
        {
            var dataSource = Guid.NewGuid().ToString();

            var serviceCollection = new ServiceCollection()
                .AddPooledDbContextFactory<CustomerDbContext>(
                    options => options.UseInMemoryDatabase($"Data Source={dataSource}.db"))
                .AddScoped<CustomerDbContext>(
                    p => p.GetRequiredService<IDbContextFactory<CustomerDbContext>>().CreateDbContext())
                .AddScoped<ICustomerRepository, CustomerRepository>()
                .AddScoped<ICustomerService, CustomerService.Services.CustomerService>()
                .AddGraphQL()
                .AddType<CustomerStatusType>()
                .AddQueryType<CustomerQueriesType>()
                .AddMutationType<CustomerMutationsType>()
                .AddProjections();

            _schema = await serviceCollection.BuildSchemaAsync();
            _requestExecutor = await serviceCollection.BuildRequestExecutorAsync();
        }
    }
}