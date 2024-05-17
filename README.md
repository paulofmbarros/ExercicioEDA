## Descrição

O Curso Full Cycle é uma formação completa para fazer com que pessoas desenvolvedoras sejam capazes de trabalhar em projetos expressivos sendo capazes de desenvolver aplicações de grande porte utilizando de boas práticas de desenvolvimento.

---

## Desafio

Desenvolva um microsserviço em sua linguagem de preferência que seja capaz de receber via Kafka os eventos gerados pelo microsserviço "Wallet Core" e persistir no banco de dados os balances atualizados para cada conta.

Crie um endpoint: `/balances/{account_id}` que exibe o balance atualizado.

Requisitos para entrega:
- [x] Tudo deve rodar via Docker / Docker-compose
- [x] Com um único docker-compose up -d todos os microsserviços, incluindo o da wallet core precisam estar disponíveis para que possamos fazer a correção.
- [x] Não esqueça de rodar migrations e popular dados fictícios em ambos bancos de dados (wallet core e o microsserviço de balances) de forma automática quando os serviços subirem.
- [x] Gere o arquivo ".http" para realizarmos as chamadas em seu microsserviço da mesma forma que fizemos no microsserviço "wallet core"
- [x] Disponibilize o microsserviço na porta: 3003.

---

## Instruções

Executar o comando `docker compose up -d` com isso as imagens serão baixadas e executadas.

Após todos containers estiverem em rodando, inicie as aplicações:

### Serviço de Wallet

Execute o comando `docker compose exec <container_name> bash` após acessar o container execute o comando `go run cmd/walletcore/main.go` ele irá rodas as migrations e subir o sevidor na porta `3000`.

### Serviço de Balances

Ao executar o comando `docker compose up -d` ja com dados pré-populados na base de dados de balances. O servidor irá subir na porta `3003`.

Os dois serviços já tem seu arquivo de `api/client.http` já com os `IDs` corretos, mas nada impede te criar novos registros e usar os mesmos.

---
