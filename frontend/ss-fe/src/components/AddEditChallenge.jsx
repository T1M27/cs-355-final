import {useState, useEffect} from "react";
import APIService from "../services/APIService";
import { useParams } from "react-router-dom";

export default function AddEditChallenge({onSuccess, onError, challenge}){
    const [title,setTitle] = useState(challenge ? challenge.title : '');
    const [description,setDescription] = useState(challenge ? challenge.description : '');
    const [difficulty, setDifficulty] = useState(challenge ? challenge.difficulty : '');
    const [tags,setTags] = useState(challenge ? challenge.tags : '');
    const [postedBy, setPostedBy] = useState(challenge ? challenge.postedBy : '');
    const { challengeId } = useParams();

    function handleSubmit(event){
        event.preventDefault();
        if(challenge){
            updateChallenge();
        } else {
            addNewChallenge();
        }
    }
    function addNewChallenge(){
        const challengeData = {title, description, difficulty, tags, postedBy}
        APIService.addChallenge(challengeData)
            .then((response) => {
                onSuccess();
                clearForm();
            })
            .catch((error) => {
                onError('Sorry, could not add that challenge.');
            })
    }

    function clearForm(){
        setTitle('');
        setDescription('');
        setDifficulty('');
        setTags('');
        setPostedBy('');
    }

    function updateChallenge(){
        if(challenge.id != challengeId){
            onError("You don't belong here!")
            return;
        }
        const challengeData = {...challenge,title,description,difficulty,tags,postedBy};
        APIService.updateChallenge(challengeData)
            .then((response) => {
                onSuccess();
                clearForm();
            })
            .catch((error) => {

            })
    }
    useEffect(() => {
        if(challenge){
            setTitle(card.title);
            setDescription(card.description);
            setDifficulty(card.difficulty);
            setTags(card.tags);
            setPostedBy(card.postedBy);
        }
    },[challenge])
    return (
        <>
        <section>
            <h2>Challenge Form</h2>
            <form onSubmit={handleSubmit}>
                <div>
                    <label>Title: </label>
                    <input type="text" placeholder="Challenge Title" value={title} onChange={(e) => setTitle(e.target.value)}/>
                </div>
                <div>
                    <label>Description: </label>
                    <input type="text" placeholder="Challenge Description" value={description} onChange={(e) => setDescription(e.target.value)}/>
                </div>
                <div>
                    <label>Difficulty: </label>
                    <input type="text" placeholder="Challenge Difficulty" value={difficulty} onChange={(e) => setDifficulty(e.target.value)}/>
                </div>
                <div>
                    <label>Tags: </label>
                    <input type="text" placeholder="Challenge Tags" value={tags} onChange={(e) => setTags(e.target.value)}/>
                </div>
                <div>
                    <label>Posted By: </label>
                    <input type="text" placeholder="Poster" value={postedBy} onChange={(e) => setPostedBy(e.target.value)}/>
                </div>
                <div>
                    <button type="submit">Submit</button>
                </div>
            </form>
        </section>
        </>
    )
}
