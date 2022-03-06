docker build . -t tcc-db

docker run -p 1433:1433 --name tcc-db -hostname tcc-db -d tcc-db