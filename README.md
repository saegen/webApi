# webApi

This is a project whose main purpose is educational. The objective as such is to enhance, enrich as well as sharpen my personal skill set in all things concerning Software Development.
That means it will most likely never end, it will be over engineered and it will aim for the stars.
So far it includes: 
Hard parts: Web API, WCF, Entity Frame work, Swagger, SQL, Git
Softish parts: Architecture i.e. design, refactoring, REST(ful), IDE and tooling, GitHub

Todo:
- [ ] Change User and Subscriptions relation from 1->* to \*->\* i.e. many to many relationship
   - [x] Change database model
      - [ ] ~~Get database under version control, i.e .mdf file, Use DACPAC instead~~
      - [ ] ~~Experiment with dacpac~~
   - [x] Create and redesign of API calls
    - [ ] Change DataService according to new design
       - [ ] Implement new methods to Subscribe, Unsubscribe, GetSubscriptionUsers
       - [x] Implement and refactor Subscription and User methods to pure CRUD
    - [ ] Refactor User and Sub controllers to use new Repo methods
    - [ ] Add AdminServcie for new subscribe/unsubscribe functionality
    - [ ] Add admin controller for new subscribe/unsubscribe functionality
   - [ ] Add AdminRepo for new subscribe/unsubscribe functionality
- [ ] ~~Put everything in Docker~~
- [ ] Put everything in Azure
- [ ] Make Controllers Async
- [ ] Look at service bus. "Needed" after many to many change when bypassing Admin 
- [ ] Introduce error handling per level.
         - [ ] Design error handling mechanism
         - [ ] Implement logging with Log4Net at DataService Level
         - [ ] Implement logging at WebApi Level
- [ ] Hide internal id's from WebApi. i.e Refactor ResultDTOs 
- [ ] Make Angular 2+ front
    

Short descpription of the architecture. Currently 3 layers, (with front-end clients it will be 4). 
Layer: 
  - 1 Back-end is SqlServer, 3 tables User <- UserSub -> Subs
  - 2 WCF dataservice,EF: code-first. Is hosted on IIS to keep it running. 3 services Users,Subs,Admin
  - 3 WebApi controller.  3 controlers Users,Subs,Admin
  - (4 Todo Angular and maybe a Core.Net clients)
