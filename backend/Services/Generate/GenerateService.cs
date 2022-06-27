using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using SieGraSieMa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SieGraSieMa.Services
{
    public interface IGenerateService
    {
        public Task<IEnumerable<Team>> GenerateTeams(int amount, int tournamentId);
        public Task<bool> GenerateMatchResults(int tournamentId, int phase);
        public Task SeedBasicData();
        public Task TestAsync();
    }
    public class GenerateService : IGenerateService
    {
        private readonly SieGraSieMaContext _context;
        private readonly UserManager<User> _userManager;

        public IConfiguration Configuration { get; set; }

        public GenerateService(UserManager<User> userManager, SieGraSieMaContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
            _userManager = userManager;
        }
        public async Task<IEnumerable<Team>> GenerateTeams(int amount, int tournamentId)
        {
            List<Team> TeamsList = new();
            for (int teamCounter = 0; teamCounter < amount; teamCounter++)
            {
                //tworzenie kapitana
                var emm = RandomString(6) + "@gmail.com";
                var username = RandomString(8);
                var captain = new User
                {
                    Name = RandomString(6),
                    Surname = RandomString(8),
                    UserName = username,
                    Email = emm,
                    NormalizedEmail = emm.ToUpper(),
                    NormalizedUserName = username.ToUpper(),
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    TwoFactorEnabled = false
                };
                await _userManager.CreateAsync(captain, "bardzoTr00dneH@slo");
                await _userManager.AddToRoleAsync(captain, "User");
                //tworzenie teamu
                var team = new Team { Code = RandomString(5), Captain = captain, Name = RandomString(10) };
                //dodawanie graczy do teamu
                team.Players.Add(new Player { Team = team, User = captain });
                for (int player = 1; player < 5; player++)
                {
                    emm = RandomString(6) + "@gmail.com";
                    username = RandomString(8);
                    var user = new User
                    {
                        Name = RandomString(6),
                        Surname = RandomString(8),
                        UserName = username,
                        Email = emm,
                        NormalizedEmail = emm.ToUpper(),
                        NormalizedUserName = username.ToUpper(),
                        EmailConfirmed = true,
                        SecurityStamp = Guid.NewGuid().ToString(),
                        TwoFactorEnabled = false
                    };
                    await _userManager.CreateAsync(user, "bardzoTr00dneH@slo");
                    await _userManager.AddToRoleAsync(user, "User");
                    team.Players.Add(new Player() { Team = team, User = user });
                }
                TeamsList.Add(team);
                team.TeamInTournaments.Add(new TeamInTournament { Team = team, TournamentId = tournamentId, Paid = true });

            }
            _context.Teams.AddRange(TeamsList);
            await _context.SaveChangesAsync();
            return TeamsList;
        }
        private static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                                                    .Select(s => s[random.Next(s.Length)])
                                                    .ToArray());
        }

        public async Task<bool> GenerateMatchResults(int tournamentId, int phase)
        {
            Random r = new();
            List<Match> Matches = _context.Matches
                .Where(m => m.TournamentId == tournamentId
                        && m.TeamAwayScore == null
                        && m.TeamHomeScore == null
                        && m.Phase == phase).ToList();
            Matches.ForEach(m =>
            {
                m.TeamHomeScore = r.Next(0, 40);
                do m.TeamAwayScore = r.Next(0, 40); while (m.TeamHomeScore == m.TeamAwayScore);
            });
            _context.Matches.UpdateRange(Matches);
            return await _context.SaveChangesAsync() > 0;
            //return Matches.Select(m => new Match { TournamentId = m.TournamentId, Phase = m.Phase, MatchId = m.MatchId });
        }

        public async Task SeedBasicData()
        {
            _context.RemoveRange(_context.Matches.ToList());
            _context.RemoveRange(_context.TeamInGroups.ToList());
            _context.RemoveRange(_context.Groups.ToList());
            _context.RemoveRange(_context.MediumInAlbum.ToList());
            _context.RemoveRange(_context.Media.ToList());
            _context.RemoveRange(_context.Albums.ToList());
            _context.RemoveRange(_context.Contestants.ToList());
            _context.RemoveRange(_context.Contests.ToList());
            _context.RemoveRange(_context.Tournaments.ToList());
            _context.RemoveRange(_context.TeamInTournaments.ToList());
            _context.RemoveRange(_context.Players.ToList());
            _context.RemoveRange(_context.Teams.ToList());
            _context.RemoveRange(_context.Newsletters.ToList());
            _context.RemoveRange(_context.Logs.ToList());
            _context.RemoveRange(_context.RefreshTokens.ToList());
            //_context.RemoveRange(_context.Users.ToList());
            _context.SaveChanges();
            var users = _context.Users.Where(u => u.UserName != "admin@gmail.com").ToList();
            foreach(var u in users)
            {
                await _userManager.DeleteAsync(u);
            }
            var password = "Haslo+123";

            /*var admin = new User
            {
                Name = "Jasio",
                Surname = "Admin",
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                NormalizedEmail = "admin@gmail.com".ToUpper(),
                NormalizedUserName = "admin@gmail.com".ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString(),
                EmailConfirmed = true,
                PhoneNumber = null,
                TwoFactorEnabled = false
            };
            var result = await _userManager.CreateAsync(admin, password);
            await _userManager.AddToRoleAsync(admin, "Admin");
            await _userManager.AddToRoleAsync(admin, "Emp");
            await _userManager.AddToRoleAsync(admin, "User");*/

            var pracownik = new User
            {
                Name = "Kuba",
                Surname = "Pracownik",
                UserName = "pracownik@gmail.com",
                Email = "pracownik@gmail.com",
                NormalizedEmail = "pracownik@gmail.com".ToUpper(),
                NormalizedUserName = "pracownik@gmail.com".ToUpper(),
                EmailConfirmed = true,
                PhoneNumber = null,
                TwoFactorEnabled = false
            };
            await _userManager.CreateAsync(pracownik, password);
            await _userManager.AddToRoleAsync(pracownik, "Employee");
            await _userManager.AddToRoleAsync(pracownik, "User");

            var kapitan = new User
            {
                Name = "Taras",
                Surname = "Kapitan",
                UserName = "kapitan@gmail.com",
                Email = "kapitan@gmail.com",
                NormalizedEmail = "kapitan@gmail.com".ToUpper(),
                NormalizedUserName = "kapitan@gmail.com".ToUpper(),
                EmailConfirmed = true,
                PhoneNumber = null,
                TwoFactorEnabled = false
            };
            await _userManager.CreateAsync(kapitan, password);
            await _userManager.AddToRoleAsync(kapitan, "User");

            var gosc = new User
            {
                Name = "Anon",
                Surname = "Gość",
                UserName = "gosc@gmail.com",
                Email = "gosc@gmail.com",
                NormalizedEmail = "gosc@gmail.com".ToUpper(),
                NormalizedUserName = "gosc@gmail.com".ToUpper(),
                EmailConfirmed = true,
                PhoneNumber = null,
                TwoFactorEnabled = false
            };
            await _userManager.CreateAsync(gosc, password);
            await _userManager.AddToRoleAsync(gosc, "User");

            await _context.SaveChangesAsync();

            var teams = new List<Team>() {
            new Team() {Name = "Bogowie", Captain=kapitan, Code = "ABCDE" },
            new Team() {Name = "Demony", Captain=kapitan, Code = "BCDEF" },
            new Team() {Name = "Ancymony", Captain=kapitan, Code = "CDEFG" },
            new Team() {Name = "Gałgany", Captain=kapitan, Code = "DEFGH" },
            new Team() {Name = "Hultaje", Captain=kapitan, Code = "EFGHI" } };
            _context.Teams.AddRange(teams);
            await _context.SaveChangesAsync();

            teams.ForEach(t => t.Players.Add(new Player() { Team = t, User = kapitan }));

            var playersList = new List<User>();
            for (int i = 0; i < 20; i++)
            {
                var gracz = new User
                {
                    Name = "Kubuś",
                    Surname = "Gracz" + i,
                    UserName = "gracz" + i + "@gmail.com",
                    Email = "gracz" + i + "@gmail.com",
                    NormalizedEmail = "gracz" + i + "@gmail.com".ToUpper(),
                    NormalizedUserName = "gracz" + i + "@gmail.com".ToUpper(),
                    EmailConfirmed = true,
                    PhoneNumber = null,
                    TwoFactorEnabled = false
                };
                await _userManager.CreateAsync(gracz, password);
                await _userManager.AddToRoleAsync(gracz, "User");
                teams.ForEach(t =>
                {
                    if (t.Id % 5 == i % 5) t.Players.Add(new Player() { Team = t, User = gracz });
                });
                playersList.Add(gracz);
            }
            await _context.SaveChangesAsync();

            var tournamentsList = new List<Tournament>{
                new Tournament()
            {
                Name = "SieGra SieMa 3x3 tournament - Gramy dla Fundacji Tymmmczasy",
                Address = "Białołęcki Ośrodek Sportu, Strumykowa 21, 03-138 Warszawa",
                Description = "<p>Siema<img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/t50/1/16/2757.png\" alt=\"❗️\" height=\"16\" width=\"16\"></p><p><img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/t33/1/16/2705.png\" alt=\"✅\" height=\"16\" width=\"16\">Po raz kolejny zmierzymy się w koszykówce 3x3 na VIII edycji turnieju SieGra SieMa<img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/tdf/1/16/1f51c.png\" alt=\"🔜\" height=\"16\" width=\"16\"><img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/tb8/1/16/1f3c0.png\" alt=\"🏀\" height=\"16\" width=\"16\">&nbsp;Tym razem zagramy dla naszych kochanych czworonogów i wspomożemy&nbsp;<a href=\"https://www.facebook.com/tyMMMczasy\" rel=\"noopener noreferrer\" target=\"_blank\" style=\"background-color: transparent; color: var(--primary-text);\">Fundacja Tymmmczasy</a>&nbsp;<img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/t6c/1/16/2764.png\" alt=\"❤️\" height=\"16\" width=\"16\"><img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/t2f/1/16/1f436.png\" alt=\"🐶\" height=\"16\" width=\"16\"></p><p><img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/t33/1/16/2705.png\" alt=\"✅\" height=\"16\" width=\"16\">Liczna drużyn jak może wziąć udział to 49 zespołów, wiec polecamy zapisywać się jak najszybciej zwłaszcza ze zostało już tylko mniej niż połowa miejsc<img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/t50/1/16/2757.png\" alt=\"❗️\" height=\"16\" width=\"16\"></p><p><img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/t50/1/16/2757.png\" alt=\"❗️\" height=\"16\" width=\"16\"><img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/tb8/1/16/1f3c0.png\" alt=\"🏀\" height=\"16\" width=\"16\"><img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/t50/1/16/2757.png\" alt=\"❗️\" height=\"16\" width=\"16\">Link do zapisów:&nbsp;<a href=\"https://siegrasiema.eu/zapisy-na-turniej/\" rel=\"noopener noreferrer\" target=\"_blank\" style=\"background-color: transparent; color: var(--blue-link);\">https://siegrasiema.eu/zapisy-na-turniej/</a></p><p><img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/t33/1/16/2705.png\" alt=\"✅\" height=\"16\" width=\"16\">Co się będzie działo na evencie?</p><p><img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/t9e/1/16/27a1.png\" alt=\"➡️\" height=\"16\" width=\"16\">&nbsp;wielki turniej 3x3<img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/tb8/1/16/1f3c0.png\" alt=\"🏀\" height=\"16\" width=\"16\"></p><p><img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/t9e/1/16/27a1.png\" alt=\"➡️\" height=\"16\" width=\"16\">&nbsp;konkursy indywidualne<img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/t3d/1/16/1f3c5.png\" alt=\"🏅\" height=\"16\" width=\"16\"></p><p><img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/t9e/1/16/27a1.png\" alt=\"➡️\" height=\"16\" width=\"16\">&nbsp;oprawa muzyczna&nbsp;<img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/t7e/1/16/1f3a4.png\" alt=\"🎤\" height=\"16\" width=\"16\"></p><p><img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/t9e/1/16/27a1.png\" alt=\"➡️\" height=\"16\" width=\"16\">&nbsp;relacja Foto<img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/tde/1/16/1f4f8.png\" alt=\"📸\" height=\"16\" width=\"16\"></p><p><img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/t9e/1/16/27a1.png\" alt=\"➡️\" height=\"16\" width=\"16\">&nbsp;woda/izotoniki/batony/owoce na miejscu<img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/tbd/1/16/1f34f.png\" alt=\"🍏\" height=\"16\" width=\"16\"><img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/tf7/1/16/1f36b.png\" alt=\"🍫\" height=\"16\" width=\"16\"></p><p><img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/t9e/1/16/27a1.png\" alt=\"➡️\" height=\"16\" width=\"16\">Zbiórka kocy/zabawek/karmy dla Psów i kotów<img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/t2f/1/16/1f436.png\" alt=\"🐶\" height=\"16\" width=\"16\"><img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/taa/1/16/1f431.png\" alt=\"🐱\" height=\"16\" width=\"16\"></p><p><img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/t9e/1/16/27a1.png\" alt=\"➡️\" height=\"16\" width=\"16\">&nbsp;i wiele wiele więcej (o wszystkim będziemy informować na bieżąco)<img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/t50/1/16/1f525.png\" alt=\"🔥\" height=\"16\" width=\"16\"></p><p><img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/t33/1/16/2705.png\" alt=\"✅\" height=\"16\" width=\"16\">&nbsp;O co zagracie?</p><p><img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/t94/1/16/1f947.png\" alt=\"🥇\" height=\"16\" width=\"16\">&nbsp;800PLN do&nbsp;<a href=\"https://www.facebook.com/skstore.warsaw/\" rel=\"noopener noreferrer\" target=\"_blank\" style=\"background-color: transparent; color: var(--primary-text);\">Kicks Store</a>&nbsp;oraz voucher na wykonanie własnych strojów od&nbsp;<a href=\"https://www.facebook.com/uffo.sport\" rel=\"noopener noreferrer\" target=\"_blank\" style=\"background-color: transparent; color: var(--primary-text);\">UFFO.PL</a></p><p><img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/t15/1/16/1f948.png\" alt=\"🥈\" height=\"16\" width=\"16\">600PLN od @Kickstore oraz rabat 35% do wykorzystania w&nbsp;<a href=\"https://www.facebook.com/sklepkoszykarza/\" rel=\"noopener noreferrer\" target=\"_blank\" style=\"background-color: transparent; color: var(--primary-text);\">Sklep Koszykarza</a></p><p><img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/t96/1/16/1f949.png\" alt=\"🥉\" height=\"16\" width=\"16\">400PLN od&nbsp;<a href=\"https://www.facebook.com/skstore.warsaw/\" rel=\"noopener noreferrer\" target=\"_blank\" style=\"background-color: transparent; color: var(--primary-text);\">Kicks Store</a>&nbsp;oraz pakiet koszulek również od&nbsp;<a href=\"https://www.facebook.com/skstore.warsaw/\" rel=\"noopener noreferrer\" target=\"_blank\" style=\"background-color: transparent; color: var(--primary-text);\">Kicks Store</a></p><p><img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/tb5/1/16/23f1.png\" alt=\"⏱\" height=\"16\" width=\"16\">Zegarek od&nbsp;<a href=\"https://www.facebook.com/Timex/\" rel=\"noopener noreferrer\" target=\"_blank\" style=\"background-color: transparent; color: var(--primary-text);\">Timex</a>&nbsp;dla MVP</p><p><img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/t40/1/16/1f4a5.png\" alt=\"💥\" height=\"16\" width=\"16\">Dla zwyciezców konkursów indywidualnych niespodzianki</p><p><img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/t9e/1/16/27a1.png\" alt=\"➡️\" height=\"16\" width=\"16\">Oczywiście będą rownież puchary oraz medale dla wszystkich najlepszych zawodników</p><p>Jak widzicie warto GRAĆ warto POMAGAĆ<img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/t6c/1/16/2764.png\" alt=\"❤️\" height=\"16\" width=\"16\"></p><p>Do zobaczenia<img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/t50/1/16/2757.png\" alt=\"❗️\" height=\"16\" width=\"16\"></p><p><br></p>",
                StartDate = DateTime.Parse("9.02.2020 10:00"),
                EndDate = DateTime.Parse("9.02.2020 20:00")
            },
                new Tournament()
            {
                Name = "SieGra SieMa edycja Świąteczna Turniej 3x3",
                Address = "Białołęcki Ośrodek Sportu, Strumykowa 21, 03-138 Warszawa",
                Description = "<p>Siema<img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/t50/1/16/2757.png\" alt=\"❗️\" height=\"16\" width=\"16\"></p><p>Już 8 Grudnia zapraszamy Was na ŚWIĄTECZNĄ edycję SieGra SieMa<img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/tdf/1/16/1f51c.png\" alt=\"🔜\" height=\"16\" width=\"16\"><img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/tb8/1/16/1f3c0.png\" alt=\"🏀\" height=\"16\" width=\"16\"></p><p>Znowu zmierzymy się w koszykarskim turnieju 3x3, ale będzie również bardzo dużoj zabawy, konkursów, muzyki, oraz dobrej atmosfery<img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/t7a/1/16/1f44f_1f3fc.png\" alt=\"👏🏼\" height=\"16\" width=\"16\"></p><p><img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/t50/1/16/2757.png\" alt=\"❗️\" height=\"16\" width=\"16\">Link do zapisów macie tutaj<img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/t9e/1/16/27a1.png\" alt=\"➡️\" height=\"16\" width=\"16\">&nbsp;<a href=\"https://siegrasiema.eu/zapisy-na-turniej\" rel=\"noopener noreferrer\" target=\"_blank\" style=\"background-color: transparent; color: var(--blue-link);\">https://siegrasiema.eu/zapisy-na-turniej/</a>&nbsp;(warto rejestrować się szybko, ponieważ dla pierwszych zespołów mamy promocyjną cenę wpisowego. Warto spieszyć się również dlatego, że ilość miejsc jest ograniczona).<img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/t50/1/16/2757.png\" alt=\"❗️\" height=\"16\" width=\"16\"></p><p>Podczas eventu przeprowadzona zostanie równiez zbiórka odzieży oraz żywności dla Domu Samotnej Matki&nbsp;<img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/t6c/1/16/2764.png\" alt=\"❤\" height=\"16\" width=\"16\">. Gorąca prośba o wsparcie od Was. Zapraszamy do wzięcia udziału zawodników, ale i kibiców oraz wszystkich którzy są w okolicy. Dla każdego będziemy mieli mały upominek, ale dla zespołu który przyniesie najwięcej dodatkowe 10 PKT do naszego RANKINGU.</p><p>POMAGAJMY POMAGAĆ</p><p>Warto również dodać, że tą edycją rozpoczynamy już drugi sezon naszej działalności. Oznacza to, że mamy dla Was przygotowane naprawdę niesamowite rzeczy, o których będziemy informować na bieżąco<img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/t38/1/16/1f918.png\" alt=\"🤘\" height=\"16\" width=\"16\"><img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/t50/1/16/1f525.png\" alt=\"🔥\" height=\"16\" width=\"16\"></p><p>CO BĘDZIE SIĘ DZIAŁO:</p><p><img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/t33/1/16/2705.png\" alt=\"✅\" height=\"16\" width=\"16\">&nbsp;Wielki Turniej 3x3<img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/tb8/1/16/1f3c0.png\" alt=\"🏀\" height=\"16\" width=\"16\"></p><p><img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/t33/1/16/2705.png\" alt=\"✅\" height=\"16\" width=\"16\">&nbsp;Konkursy indywidualne, oraz konkurs drużynowy<img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/t40/1/16/1f4a5.png\" alt=\"💥\" height=\"16\" width=\"16\"></p><p><img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/t33/1/16/2705.png\" alt=\"✅\" height=\"16\" width=\"16\">&nbsp;Fajne świąteczne prezenty do kupienia<img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/t50/1/16/1f525.png\" alt=\"🔥\" height=\"16\" width=\"16\"></p><p><img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/t33/1/16/2705.png\" alt=\"✅\" height=\"16\" width=\"16\">&nbsp;Super nagrody, które będziemy \"odsłaniać\" w nadchodzących tygodniach<img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/t6c/1/16/2764.png\" alt=\"❤️\" height=\"16\" width=\"16\"></p><p><img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/t33/1/16/2705.png\" alt=\"✅\" height=\"16\" width=\"16\">&nbsp;Relelacja FOTO oraz VIDEO z dnia turniejowego<img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/t7b/1/16/1f44c.png\" alt=\"👌\" height=\"16\" width=\"16\"></p><p><img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/t33/1/16/2705.png\" alt=\"✅\" height=\"16\" width=\"16\">Opaski SieGra SieMa dla każdego kto będzie u nas po raz pierwszy<img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/t7f/1/16/1f60a.png\" alt=\"😊\" height=\"16\" width=\"16\"></p><p><img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/t33/1/16/2705.png\" alt=\"✅\" height=\"16\" width=\"16\">&nbsp;I wiele, wiele więcej</p><p>Nie pozostaje nam nic innego niż raz jeszcze gorąco zaprosić do GRY<img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/t50/1/16/2757.png\" alt=\"❗️\" height=\"16\" width=\"16\"></p><p>Do zobaczenia<img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/tb8/1/16/1f3c0.png\" alt=\"🏀\" height=\"16\" width=\"16\"></p><p><br></p>",
                StartDate = DateTime.Parse("8.12.2019 09:00"),
                EndDate = DateTime.Parse("8.12.2019 17:00")
            },
                new Tournament()
            {
                Name = "Wielki FINAŁ sezonu SieGra SieMa",
                Address = "Ursynowskie Centrum Sportu i Rekreacji",
                Description = "<p>Siema&nbsp;<img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/t40/1/16/1f4a5.png\" alt=\"💥\" height=\"16\" width=\"16\"></p><p>Już 28 września odbędzie się Największe koszykarskie wydarzenie roku jakim jest finał tego sezonu SieGra SieMa. 28 września na Arenie Ursynów zmierzy się ponad 80 zespołów, a pula nagród wyniesie blisko 15 000 zł!</p><p><img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/t50/1/16/2757.png\" alt=\"❗️\" height=\"16\" width=\"16\"></p><p>I miejsce→4500zł*</p><p>II miejsce→3000zł*</p><p>III miejsce→1500zł*</p><p><img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/t50/1/16/2757.png\" alt=\"❗️\" height=\"16\" width=\"16\"></p><p>*- do zrealizowania w&nbsp;<a href=\"https://www.facebook.com/sklepkoszykarza/\" rel=\"noopener noreferrer\" target=\"_blank\" style=\"background-color: transparent; color: var(--primary-text);\">Sklep Koszykarza</a></p><p>Link do zapisów:&nbsp;<a href=\"https://siegrasiema.eu/zapisy-na-turniej/\" rel=\"noopener noreferrer\" target=\"_blank\" style=\"background-color: transparent; color: var(--blue-link);\">https://siegrasiema.eu/zapisy-na-turniej/</a></p><p>A co się będzie działo na imprezie?</p><p>→ Na początek rejestracja zespołów i odbiór starterpacków ufundowanych przez&nbsp;<a href=\"https://www.facebook.com/skstore.warsaw/\" rel=\"noopener noreferrer\" target=\"_blank\" style=\"background-color: transparent; color: var(--primary-text);\">Kicks Store</a><img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/te7/1/16/1f4aa_1f3fb.png\" alt=\"💪🏻\" height=\"16\" width=\"16\"></p><p>→ Knockout challenge, który rozgrzeje nam imprezę od samego początku<img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/t50/1/16/1f525.png\" alt=\"🔥\" height=\"16\" width=\"16\"></p><p>→ Rozgrywki grupowe</p><p>→ Pokaz wsadów i freestyl'u (będzie niespodzianka)<img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/t77/1/16/203c.png\" alt=\"‼️\" height=\"16\" width=\"16\"></p><p>→ NIESAMOWITY KONKURS WSADÓW, na którym do wygrania będzie 1500 zł<img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/t40/1/16/1f4a5.png\" alt=\"💥\" height=\"16\" width=\"16\">( 1000 PLN do&nbsp;<a href=\"https://www.facebook.com/sklepkoszykarza/\" rel=\"noopener noreferrer\" target=\"_blank\" style=\"background-color: transparent; color: var(--primary-text);\">Sklep Koszykarza</a>&nbsp;500 PLN od&nbsp;<a href=\"https://www.facebook.com/siegrasiema.inicjatywa/\" rel=\"noopener noreferrer\" target=\"_blank\" style=\"background-color: transparent; color: var(--primary-text);\">SieGra SieMa</a>)</p><p>→ 3pt battle, nowość jakiej nie było nigdzie indziej, będziecie zachwyceni</p><p>→ Faza pucharowa&nbsp;<img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/tbe/1/16/1f3c6.png\" alt=\"🏆\" height=\"16\" width=\"16\"></p><p>→ Wielki finał&nbsp;<img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/tb8/1/16/1f3c0.png\" alt=\"🏀\" height=\"16\" width=\"16\"></p><p>Oczywiście podczas imprezy będziemy znów zbierać pieniądze, aby kolejny raz pomóc uratować życie i zdrowie chorego dziecka. Tym małym wojownikiem tym razem jest Patryk chory na Klątwę Ondyny. Jego historie możecie przeczytać tutaj:&nbsp;<a href=\"https://www.siepomaga.pl/siegrasiemadlapatryka\" rel=\"noopener noreferrer\" target=\"_blank\" style=\"background-color: transparent; color: var(--blue-link);\">https://www.siepomaga.pl/siegrasiemadlapatryka</a>&nbsp;<img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/t6c/1/16/2764.png\" alt=\"❤️\" height=\"16\" width=\"16\"></p><p>Podsumowując wiemy jedno, to będzie NAJLEPSZE i NAJWIĘKSZE koszykarskie wydarzenie w POLSCE. Znaczy to, że nie może Was u nas zabraknąć&nbsp;<img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/tf9/1/16/1f44f_1f3fb.png\" alt=\"👏🏻\" height=\"16\" width=\"16\"><img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/t6c/1/16/2764.png\" alt=\"❤️\" height=\"16\" width=\"16\"><img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/t50/1/16/1f525.png\" alt=\"🔥\" height=\"16\" width=\"16\"></p><p>Partnerzy imprezy:</p><p><a href=\"https://www.facebook.com/skstore.warsaw/\" rel=\"noopener noreferrer\" target=\"_blank\" style=\"background-color: transparent; color: var(--primary-text);\">Kicks Store</a>&amp;&nbsp;<a href=\"https://www.facebook.com/sklepkoszykarza/\" rel=\"noopener noreferrer\" target=\"_blank\" style=\"background-color: transparent; color: var(--primary-text);\">Sklep Koszykarza</a></p><p><a href=\"https://www.facebook.com/AvivaPoland/\" rel=\"noopener noreferrer\" target=\"_blank\" style=\"background-color: transparent; color: var(--primary-text);\">Aviva</a></p><p><a href=\"https://www.facebook.com/dailyfruitspl\" rel=\"noopener noreferrer\" target=\"_blank\" style=\"background-color: transparent; color: var(--primary-text);\">Dailyfruits | www.dailyfruits.pl | owoce w pracy</a></p><p><a href=\"https://www.facebook.com/CzasInspiracji/\" rel=\"noopener noreferrer\" target=\"_blank\" style=\"background-color: transparent; color: var(--primary-text);\">Timex Poland</a></p><p><a href=\"https://www.facebook.com/warszawa.ursynow/\" rel=\"noopener noreferrer\" target=\"_blank\" style=\"background-color: transparent; color: var(--primary-text);\">Dzielnica Ursynów m.st. Warszawy</a></p><p><a href=\"https://www.facebook.com/Ursynowskie-Centrum-Sportu-i-Rekreacji-210543632293001/\" rel=\"noopener noreferrer\" target=\"_blank\" style=\"background-color: transparent; color: var(--primary-text);\">Ursynowskie Centrum Sportu i Rekreacji</a></p><p><a href=\"https://www.facebook.com/pasjaAZS/\" rel=\"noopener noreferrer\" target=\"_blank\" style=\"background-color: transparent; color: var(--primary-text);\">AZS Akademicki Związek Sportowy</a></p><p>Do zobaczenia&nbsp;<img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/tdf/1/16/1f51c.png\" alt=\"🔜\" height=\"16\" width=\"16\"><img src=\"https://static.xx.fbcdn.net/images/emoji.php/v9/tb8/1/16/1f3c0.png\" alt=\"🏀\" height=\"16\" width=\"16\"></p><p><br></p>",
                StartDate = DateTime.Parse("28.09.2019 9:00"),
                EndDate = DateTime.Parse("28.09.2019 19:00")
            }
            };

            var contestList = new List<Contest>
            {
                new Contest(){Tournament=tournamentsList[0],Name="3Pt battle"},
                new Contest(){Tournament=tournamentsList[0],Name="Skills challenge"},
                new Contest(){Tournament=tournamentsList[0],Name="Dunk contest"},
                new Contest(){Tournament=tournamentsList[1],Name="3Pt battle"},
                new Contest(){Tournament=tournamentsList[1],Name="Skills challenge"},
                new Contest(){Tournament=tournamentsList[1],Name="Dunk contest"},
                new Contest(){Tournament=tournamentsList[2],Name="3Pt battle"},
                new Contest(){Tournament=tournamentsList[2],Name="Skills challenge"},
                new Contest(){Tournament=tournamentsList[2],Name="Dunk contest"},
            };
            var contestantList = new List<Contestant>
            {
                new Contestant(){Contest=contestList[0],User=playersList[0], Points=4},
                new Contestant(){Contest=contestList[0],User=playersList[1], Points=7},
                new Contestant(){Contest=contestList[0],User=playersList[2], Points=5},
                new Contestant(){Contest=contestList[0],User=playersList[3], Points=9},
                new Contestant(){Contest=contestList[1],User=playersList[0], Points=4},
                new Contestant(){Contest=contestList[1],User=playersList[1], Points=7},
                new Contestant(){Contest=contestList[3],User=playersList[0], Points=4},
                new Contestant(){Contest=contestList[3],User=playersList[1], Points=7},
                new Contestant(){Contest=contestList[3],User=playersList[2], Points=5},
                new Contestant(){Contest=contestList[3],User=playersList[3], Points=9},
                new Contestant(){Contest=contestList[4],User=playersList[0], Points=4},
                new Contestant(){Contest=contestList[4],User=playersList[1], Points=7},
                new Contestant(){Contest=contestList[6],User=playersList[0], Points=4},
                new Contestant(){Contest=contestList[6],User=playersList[1], Points=7},
                new Contestant(){Contest=contestList[6],User=playersList[2], Points=5},
                new Contestant(){Contest=contestList[6],User=playersList[3], Points=9},
                new Contestant(){Contest=contestList[7],User=playersList[0], Points=4},
                new Contestant(){Contest=contestList[7],User=playersList[1], Points=7},
            };

            _context.Tournaments.AddRange(tournamentsList);
            _context.Contests.AddRange(contestList);



            await _context.SaveChangesAsync();
        }
        public async Task TestAsync()
        {
            var teams = _context.Teams.ToList();
            teams.ForEach(t =>
            {
                t.Code = RandomString(5);
            });
            _context.UpdateRange(teams);
            await _context.SaveChangesAsync();
        }

    }
}
