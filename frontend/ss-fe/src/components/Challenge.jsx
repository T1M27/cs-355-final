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
                <h2>{challenge.title}</h2>
                <p>{challenge.difficulty}</p>
                <p>{challenge.postedBy}</p>
                <p>{challenge.description}</p>
                <p>{challenge.tags}</p>
            </>
        )
}