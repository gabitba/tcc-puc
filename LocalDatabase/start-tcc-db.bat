docker build . -t tcc-db

docker run -p 1433:1433 -d tcc-db