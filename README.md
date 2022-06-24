# GetInto Software
Software para alocação de pessoas aos projetos internos de uma empresa.

<br>

## Motivação
O software surge para que administradores cadastrem novos projetos e vagas. Outros colaboradores poderão demonstrar interesse nas vagas disponíveis.

<br>

## Características
#### Ferramentas
.NET Core 6.0.3 <br>
JwtBearer 6.0.5<br>
AspNetCore Identity 6.0.5<br>

#### Arquitetura
Persistência genérica: [GeralPersist.cs](https://github.com/mateus-ceccagno/GetInto-srv/blob/main/GetInto.Persistence/GeralPersist.cs) <br>
Camadas: [API](https://github.com/mateus-ceccagno/GetInto-srv/tree/main/GetInto.API), [Application](https://github.com/mateus-ceccagno/GetInto-srv/tree/main/GetInto.Application), [Persistence](https://github.com/mateus-ceccagno/GetInto-srv/tree/main/GetInto.Persistence), [Domain](https://github.com/mateus-ceccagno/GetInto-srv/tree/main/GetInto.Domain) <br>
Paginação: [PageList.cs](https://github.com/mateus-ceccagno/GetInto-srv/blob/main/GetInto.Persistence/Pagination/PageList.cs)
