# Pega a imagem do sql server 2019
FROM mcr.microsoft.com/mssql/server:2019-latest

# Cria diretórios
RUN mkdir -p /usr/scripts
WORKDIR /usr/scripts

# Copia scripts de inicialização para dentro do container
COPY ./scripts /usr/scripts

# Seta variáveis de ambiente
ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=Gabs!Tcc
ENV MSSQL_PID=Express

# Roda script de inicialização no mesmo tempo que o sql Server
CMD /bin/bash ./entrypoint.sh