uruchamiamy z cmd za pomocą "docker compose up"
w http://localhost:8183/index.php logujemy się i tworzymy nową bazę danych:
nazwa: SieGraSieMa
kodowanie: utf8_polish_ci

po utworzeniu dodajemy użytkownika
user=siegra
password=siema
ze wszystkimi uprawnieniami

po dodaniu użytkownika musimy zaktualizować bazę danych za pomocą komendy "update-database" wywołanej na backendzie (najlepiej z konsoli pakietów nuget)