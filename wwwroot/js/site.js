function hideMenu() {

    var business = this.document.getElementById('business');
    var science = this.document.getElementById('science');
    var sports = this.document.getElementById('sports');
    var technology = this.document.getElementById('technology');
    var entertainment = this.document.getElementById('entertainment');

    if (this.window.innerWidth <= 780) {
        business.innerHTML = "<i class=\"fas fa-comment-dollar\"></i>";
        science.innerHTML = "<i class=\"fas fa-atom\"></i>";
        sports.innerHTML = "<i class=\"fas fa-basketball-ball\"></i>";
        technology.innerHTML = "<i class=\"fa fa-flask\"></i>";
        entertainment.innerHTML = "<i class=\"fas fa-guitar\"></i>";
    } else {
        business.innerHTML = "<i class=\"fas fa-comment-dollar\"></i> Οικονομία";
        science.innerHTML = "<i class=\"fas fa-atom\"></i> Επιστήμη";
        sports.innerHTML = "<i class=\"fas fa-basketball-ball\"></i> Αθλητικά";
        technology.innerHTML = "<i class=\"fa fa-flask\"></i> Τεχνολογία";
        entertainment.innerHTML = "<i class=\"fas fa-guitar\"></i> Διασκέδαση";
    }

}

window.addEventListener('resize', hideMenu);
hideMenu();

/*var science = document.getElementById('science');
science.className = "active_menu";*/