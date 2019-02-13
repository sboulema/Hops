var db = firebase.firestore();

function saveToFirebase(element) {
    var firebaseCode = localStorage.getItem("firebaseCode");

    if (firebaseCode === null || firebaseCode === "") {   
        db.collection("inventory").add(element)
        .then(function(docRef) {
            localStorage.setItem("firebaseCode", docRef.id);
        })
        .catch(function(error) {
            console.error("Error adding document: ", error);
        });
    }
    else {
        db.collection("inventory").doc(firebaseCode).set(element)
        .then(function(docRef) {
            // saved
        })
        .catch(function(error) {
            console.error("Error adding document: ", error);
        });
    }
}

async function loadFromFirebase() {
    var firebaseCode = localStorage.getItem("firebaseCode");
    if (firebaseCode === null || firebaseCode === "") return;

    var docRef = db.collection("inventory").doc(firebaseCode); 
    var doc = await docRef.get();

    if (doc.exists) {
        return doc.data();
    } else {
        console.log("No such document!");
    }
}

function deleteAllData() {
    localStorage.clear();
}

function getFirebaseCode() {
    return localStorage.getItem("firebaseCode"); 
}

function setFirebaseCode(code) {
    localStorage.setItem("firebaseCode", code);
}

function importToFireBase(event) { 
    var file = event.files[0];

    if (file) {
        var r = new FileReader();
        r.onload = function (e) {
            saveToFirebase(JSON.parse(e.target.result));      
        }
        r.readAsText(file);
    } else {
        alert("Failed to load file");
    }
}