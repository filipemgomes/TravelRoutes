# Rota de Viagem

## Escolha a rota de viagem mais barata, independente da quantidade de conexões.

O sistema permite registrar novas rotas e consultar a melhor opção disponível.

---

## Como Executar a Aplicação

### Compilar o projeto:
```sh
dotnet build
```

### Executar a aplicação:
```sh
dotnet run --project ConsoleApp
```

### Interagir com o programa:

- **Registrar uma nova rota** digitando: `Origem,Destino,Valor`
- **Consultar a melhor rota** digitando: `Origem-Destino`

#### Exemplo de uso:
```
Digite a rota: GRU-CDG
Melhor Rota: GRU - BRC - SCL - ORL - CDG ao custo de $40
```

---

## Estrutura dos Arquivos

O projeto segue **Clean Architecture**, separando claramente as responsabilidades:

```
Projeto/
│── **Domain/**          # Regras de Negócio (Entidades e Exceptions)
│── **Application/**     # Casos de Uso (UseCases)
│── **Infrastructure/**  # Persistência e Repositórios
│── **ConsoleApp/**      # Interface do Usuário
│── **UnitTests/**       # Testes Unitários
```

---

## Decisões de Design

### Uso da **Clean Architecture**

- **Camada Domain**: Contém a entidade `Route` e as exceções personalizadas (`DomainException`).
- **Camada Application**: Implementa os casos de uso `AddRouteUseCase` e `GetBestRouteUseCase`, aplicando a regra de menor custo.
- **Camada Infrastructure**: Implementa a persistência usando **JSON** para armazenar as rotas, garantindo simplicidade para demonstração.
- **Camada ConsoleApp**: Interage com o usuário e permite entrada de dados.

### Persistência com JSON

Optamos por armazenar as rotas em um arquivo **JSON** para simplificar a demonstração. Entretanto, poderíamos facilmente substituir isso por um banco de dados, como **SQLite** ou **PostgreSQL**, caso fosse necessário um armazenamento mais robusto.

### Tratamento de Erros com **DomainException**

Implementamos `DomainException` para validar corretamente as entidades antes de serem persistidas, garantindo integridade nos dados.

---

## Testes Unitários

Foram implementados testes unitários seguindo boas práticas de mercado:

- **Testes na camada Domain** para validar regras de negócio da entidade `Route`.
- **Testes na camada Application** garantindo que os **UseCases** funcionam corretamente.
- **Testes na camada Infrastructure** validando a persistência e leitura de dados JSON.

---

## Diferenciais Implementados

✅ **Separação completa entre as camadas**, garantindo extensibilidade e manutenção simplificada.  
✅ **Uso de Exceptions personalizadas** (`DomainException`) para validação das entidades.  
✅ **Testes unitários completos**, cobrindo os principais fluxos de operação.  
✅ **Persistência simples com JSON**, permitindo fácil substituição por um banco de dados.  

---

## Requisitos Atendidos

✔ **Registro de novas rotas** e persistência para consultas futuras.  
✔ **Consulta da melhor rota baseada no menor custo**, independente da quantidade de conexões.  
✔ **Testes unitários implementados** conforme as boas práticas.  
✔ **Estrutura do código seguindo Clean Architecture e SOLID**.  

---

## Observação

Conforme o desafio, **não utilizamos o algoritmo de Dijkstra**, mas sim uma abordagem alternativa para encontrar a melhor rota baseada no menor custo.
