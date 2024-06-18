import { Outlet } from "react-router-dom";
import ProductFilter from "./components/ProductFilter";

const Index: React.FC = () => {
    return (<>
        <ProductFilter >
            <Outlet />
        </ProductFilter>
    </>);
};

export default Index;