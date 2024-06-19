import { useParams } from "react-router-dom";
import ProductFilter from "./components/ProductFilter";

const Index: React.FC = () => {
    const { categoryId } = useParams();
    const searchStr = categoryId ?? '';

    return (<>
        <ProductFilter searchStr={searchStr} />
    </>);
};

export default Index;