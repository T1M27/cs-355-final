import APIService from "../services/APIService";

export default function Challenge({challenge, onDelete, onEdit}){
    function handleDelete(){
        APIService.deleteCard(card.id)
            .then((response) => {
                onDelete();
            })
            .catch((error) => {

            })
        }
    
        return (
            <>
                <h3>{challenge.title}</h3>
                <p>Difficulty: {challenge.difficulty}</p>
                <p>Posted by: {challenge.postedBy}</p>
                <p>Description: {challenge.description}</p>
                <p>Tags: {challenge.tags}</p>
            </>
        )
}